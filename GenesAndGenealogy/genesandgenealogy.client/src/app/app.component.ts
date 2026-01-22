import { Component, OnInit } from '@angular/core';
import { GedcomService } from './services/gedcom.service';
import { TreeModel } from '../view-models/TreeModel';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css',
  providers: [GedcomService]
})
export class AppComponent implements OnInit {
  public treeModel!: TreeModel;

  constructor(private gedcomService: GedcomService) {
  }

  ngOnInit() {
    this.getTree();
  }

  getTree() {
    this.gedcomService.getTree().subscribe(
      (treeModel) => {
        this.treeModel = treeModel;
      },
      (error) => {
        console.error(error);
      });
  }

  title = 'genesandgenealogy.client';
}
