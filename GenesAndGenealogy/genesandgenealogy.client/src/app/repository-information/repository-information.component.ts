import { Component, inject, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
  @Input() individualXref!: string;
  @Input() repository!: RepositoryModel;
  @Input() source!: SourceModel;

  constructor(private gedcomService: GedcomService) {
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      const repositoryXref = params['repositoryXref'];

      this.gedcomService.getRepository(repositoryXref).subscribe(
        (repository) => {
          this.repository = repository;
        },
        (error) => {
          console.error(error);
        }
      )
    });

    this.activatedRoute.queryParams.subscribe((params) => {
      const sourceXref = params['sourceXref'];
      this.individualXref = params['individualXref'];

      this.gedcomService.getSource(sourceXref).subscribe(
        (source) => {
          this.source = source;
        },
        (error) => {
          console.error(error);
        }
      )
    });
  }
}
