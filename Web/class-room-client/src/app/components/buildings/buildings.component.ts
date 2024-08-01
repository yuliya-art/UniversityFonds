import { Component, OnInit, ViewChild } from '@angular/core';
import { DataBindingDirective } from '@progress/kendo-angular-grid';
import { process } from '@progress/kendo-data-query';
import { SVGIcon, fileExcelIcon, filePdfIcon } from '@progress/kendo-svg-icons';
import { BuildingService } from '../../services/building.service';
import { Building } from '../../models/building.model';
import { Router } from '@angular/router';

@Component({
    selector: 'app-buildings-component',
    templateUrl: './buildings.component.html',
    styleUrl: './buildings.component.scss'
})
export class BuildingsComponent implements OnInit {
    @ViewChild(DataBindingDirective) dataBinding?: DataBindingDirective;

    public gridData: Building[] = [];
    public gridView: Building[] = [];
    public excelIcon: SVGIcon = fileExcelIcon;
    public pdfIcon: SVGIcon = filePdfIcon;

    public mySelection: string[] = [];

    constructor(private buildingService: BuildingService, private router: Router) { }

    
    ngOnInit() {
        this.getBuildings();
      }

    getBuildings() {
        this.buildingService.getBuildings().subscribe(buildings => {
          this.gridData = buildings;
          this.gridView = buildings;
        });
      }
    
    public getField = (args: Building) => {
        return `${args.name}_${args.description}_${args.floorsNumber}_${args.address}`;
    };

    public onFilter(inputValue: string): void {
        this.gridView = process(this.gridData, {
            filter: {
                logic: 'or',
                filters: [
                    {
                        field: this.getField,
                        operator: 'contains',
                        value: inputValue
                    }
                ]
            }
        }).data;

        this.dataBinding ? (this.dataBinding.skip = 0) : null;
    }

    public createBuilding(){
        this.router.navigate(["buildings/new"])
    }
}
