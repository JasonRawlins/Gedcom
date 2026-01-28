import { Component, inject, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { combineLatest, forkJoin, of } from 'rxjs';
import { catchError, distinctUntilChanged, map, switchMap } from 'rxjs/operators';
import { GedcomService } from '../services/gedcom.service';
import { RepositoryModel } from '../../view-models/RepositoryModel';
import { SourceModel } from '../../view-models/SourceModel';

@Component({
  selector: 'app-repository-information',
  standalone: false,
  templateUrl: './repository-information.component.html',
  styleUrl: './repository-information.component.css'
})
export class RepositoryInformationComponent {
  private activatedRoute = inject(ActivatedRoute);
  individualXref: string | null = null;
  repository: RepositoryModel | null = null;
  source: SourceModel | null = null;

  constructor(private gedcomService: GedcomService) {
  }

  ngOnInit() {
    const repositoryXref$ = this.activatedRoute.paramMap.pipe(
      map(paramMap => paramMap.get('repositoryXref')),
      distinctUntilChanged()
    );

    const sourceXref$ = this.activatedRoute.queryParamMap.pipe(
      map(queryParamMap => queryParamMap.get('sourceXref')),
      distinctUntilChanged()
    );

    const individualXref$ = this.activatedRoute.queryParamMap.pipe(
      map(queryParamMap => queryParamMap.get('individualXref')),
      distinctUntilChanged()
    );

    combineLatest([repositoryXref$, sourceXref$, individualXref$]).pipe(
      switchMap(([repositoryXref, sourceXref, individualXref]) => {
        if (!repositoryXref) return of(null);

        // Get repository
        return this.gedcomService.getRepository(repositoryXref).pipe(
          // Get source
          switchMap(repository => {
            return forkJoin({
              repository: of(repository),
              source: sourceXref
                ? this.gedcomService.getSource(sourceXref)
                : of(null),
              individualXref: of(individualXref ?? null)
            });
          })
        );
      }),
      catchError(error => {
        console.error(error);
        return of(null);
      })
    ).subscribe(resultModel => {
      if (!resultModel) return;
      this.individualXref = resultModel.individualXref;
      this.repository = resultModel.repository;
      this.source = resultModel.source;
    });
  }
}
