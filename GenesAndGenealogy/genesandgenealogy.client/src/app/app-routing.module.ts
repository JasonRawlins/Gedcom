import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { TreeComponent } from './tree/tree.component';

const routes: Routes = [
  { path: '', component: TreeComponent },
  { path: '/profile:individualXref', component: ProfileComponent, title: 'Profile' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
