import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProfileHeaderComponent } from './profile-header/profile-header.component';
import { FactsComponent } from './facts/facts.component';
import { TimelineComponent } from './timeline/timeline.component';
import { ProfileSourcesComponent } from './profile-sources/profile-sources.component';
import { RelationshipsComponent } from './relationships/relationships.component';
import { RelationshipCardComponent } from './relationship-card/relationship-card.component';
import { TreeComponent } from './tree/tree.component';
import { ProfileComponent } from './profile/profile.component';
import { SourceInformationComponent } from './source-information/source-information.component';
import { RepositoryInformationComponent } from './repository-information/repository-information.component';
import { GalleryComponent } from './gallery/gallery.component';

@NgModule({
  declarations: [
    AppComponent,
    ProfileHeaderComponent,
    FactsComponent,
    TimelineComponent,
    ProfileSourcesComponent,
    RelationshipsComponent,
    RelationshipCardComponent,
    TreeComponent,
    ProfileComponent,
    SourceInformationComponent,
    RepositoryInformationComponent,
    GalleryComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
