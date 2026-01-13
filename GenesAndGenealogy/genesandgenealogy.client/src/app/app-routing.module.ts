import { NgModule } from '@angular/core';
import { RouterModule, RouterOutlet, Routes } from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { TreeComponent } from './tree/tree.component';

const routes: Routes = [
  { path: '', component: TreeComponent },
  { path: 'tree', component: TreeComponent },
  { path: 'profile/:individualXref', component: ProfileComponent, title: 'Profile' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes), RouterOutlet],
  exports: [RouterModule]
})
export class AppRoutingModule { }
