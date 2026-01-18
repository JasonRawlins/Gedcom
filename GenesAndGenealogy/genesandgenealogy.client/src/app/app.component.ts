import { Component, OnInit } from '@angular/core';
import { GedcomService } from './services/gedcom.service';
import { ProfileModel } from '../view-models/ProfileModel';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css',
  providers: [GedcomService]
})
export class AppComponent implements OnInit {
  public profile?: ProfileModel;

  constructor(private gedcomService: GedcomService) {
  }

  ngOnInit() {
    this.getProfile("@I272718948910@");
    //this.gedcomService.getIndividuals().subscribe(
    //  (individuals) => {
    //    this.individuals = individuals;
    //  },
    //  (error) => {
    //    console.error(error);
    //  });
  }

  getProfile(individualXref: string) {
    this.gedcomService.getProfile(individualXref).subscribe(
      (profileModel) => {
        this.profile = profileModel;
      },
      (error) => {
        console.error(error);
      });
  }

  title = 'genesandgenealogy.client';
}
