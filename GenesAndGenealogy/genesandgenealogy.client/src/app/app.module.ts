import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProfileHeaderComponent } from './profile-header/profile-header.component';
import { FactsComponent } from './facts/facts.component';
import { TimelineComponent } from './timeline/timeline.component';
import { SourcesComponent } from './sources/sources.component';
import { RelationshipsComponent } from './relationships/relationships.component';
import { RelationshipCardComponent } from './relationship-card/relationship-card.component';
import { TreeComponent } from './tree/tree.component';
import { ProfileComponent } from './profile/profile.component';

@NgModule({
  declarations: [
    AppComponent,
    ProfileHeaderComponent,
    FactsComponent,
    TimelineComponent,
    SourcesComponent,
    RelationshipsComponent,
    RelationshipCardComponent,
    TreeComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
