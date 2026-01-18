import { NgModule } from '@angular/core';
import { RouterModule, RouterOutlet, Routes } from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { TreeComponent } from './tree/tree.component';
import { SourceInformationComponent } from './source-information/source-information.component';
import { RepositoryInformationComponent } from './repository-information/repository-information.component';

const routes: Routes = [
  { path: '', component: TreeComponent },
  { path: 'tree', component: TreeComponent },
  { path: 'profile/:individualXref', component: ProfileComponent, title: 'Profile' },
  { path: 'source-information/:sourceXref', component: SourceInformationComponent, title: 'Source information' },
  { path: 'repository-information/:repositoryXref', component: RepositoryInformationComponent, title: 'Repository information'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes), RouterOutlet],
  exports: [RouterModule]
})
export class AppRoutingModule { }
