import { Component, OnInit } from '@angular/core';
import { ProfileModel } from '../view-models/ProfileModel';
import { GedcomService } from './gedcom.service';
import { ProfileHeaderComponent } from './profile-header/profile-header.component';

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
  }

  getProfile(indiXref: string) {
    this.gedcomService.getProfile(indiXref).subscribe(
      (profileModel) => {
        this.profile = profileModel;
      },
      (error) => {
        console.error(error);
      });
  }

  title = 'genesandgenealogy.client';
}
