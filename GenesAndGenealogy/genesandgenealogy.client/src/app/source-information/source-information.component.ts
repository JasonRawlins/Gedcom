import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { combineLatest, forkJoin, of } from 'rxjs';
import { catchError, distinctUntilChanged, map, switchMap } from 'rxjs/operators';
import { GedcomService } from '../services/gedcom.service';
import { IndividualModel } from '../../view-models/IndividualModel';
import { RepositoryModel } from '../../view-models/RepositoryModel';
import { SourceModel } from '../../view-models/SourceModel';

@Component({
  selector: 'app-source-information',
  standalone: false,
  templateUrl: './source-information.component.html',
  styleUrl: './source-information.component.css'
})
export class SourceInformationComponent {
  private activatedRoute = inject(ActivatedRoute);
  individual: IndividualModel | null = null;
  repository: RepositoryModel | null = null;
  source: SourceModel | null = null;

  constructor(private gedcomService: GedcomService) {
  }

  ngOnInit() {
    const sourceXref$ = this.activatedRoute.paramMap.pipe(
      map(paramMap => paramMap.get('sourceXref')),
      distinctUntilChanged()
    );

    const individualXref$ = this.activatedRoute.queryParamMap.pipe(
      map(queryParamMap => queryParamMap.get('individualXref')),
      distinctUntilChanged()
    );

    combineLatest([sourceXref$, individualXref$]).pipe(
      switchMap(([sourceXref, individualXref]) => {
        if (!sourceXref) return of(null);

        // Get source
        return this.gedcomService.getSource(sourceXref).pipe(
          // Get repository and individual
          switchMap(source => {
            this.source = source;

            return forkJoin({
              repository: this.gedcomService.getRepository(source.repositoryXref),
              individual: individualXref 
                ? this.gedcomService.getIndividual(individualXref)
                : of(null)
            });
          })
        );
      }),
      catchError(error => {
        console.error(error);
        return of({ repository: null, individual: null });
      })
    ).subscribe(repositoryAndIndividualResult => {
      if (!repositoryAndIndividualResult) return;
      this.repository = repositoryAndIndividualResult.repository;
      this.individual = repositoryAndIndividualResult.individual;
    });
  }
}
