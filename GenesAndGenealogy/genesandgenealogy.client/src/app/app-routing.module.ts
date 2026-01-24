import { NgModule } from '@angular/core';
import { RouterModule, RouterOutlet, Routes } from '@angular/router';
import { FactsComponent } from './facts/facts.component';
import { GalleryComponent } from './gallery/gallery.component';
import { ProfileComponent } from './profile/profile.component';
import { RepositoryInformationComponent } from './repository-information/repository-information.component';
import { SourceInformationComponent } from './source-information/source-information.component';
import { TreeComponent } from './tree/tree.component';

const routes: Routes = [
  { path: 'tree', component: TreeComponent },
  {
    path: 'profile/:individualXref',
    component: ProfileComponent,
    title: 'Profile',
    children: [
      { path: 'facts', component: FactsComponent, title: 'Facts' },
      { path: 'gallery', component: GalleryComponent, title: 'Gallery' }
    ]
  },
  { path: 'source-information/:sourceXref', component: SourceInformationComponent, title: 'Source information' },
  { path: 'repository-information/:repositoryXref', component: RepositoryInformationComponent, title: 'Repository information'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes), RouterOutlet],
  exports: [RouterModule]
})
export class AppRoutingModule { }
