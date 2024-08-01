import { Component, OnInit, ViewChild } from '@angular/core';
import { DataBindingDirective } from '@progress/kendo-angular-grid';
import { fileExcelIcon, filePdfIcon, SVGIcon } from '@progress/kendo-svg-icons';
import { ClassRoom } from '../../models/class-room.model';
import { ClassRoomService } from '../../services/class-room.service';
import { Router } from '@angular/router';
import { process } from '@progress/kendo-data-query';

@Component({
  selector: 'app-class-rooms',
  templateUrl: './class-rooms.component.html',
  styleUrl: './class-rooms.component.scss'
})
export class ClassRoomsComponent implements OnInit {
  @ViewChild(DataBindingDirective) dataBinding?: DataBindingDirective;
  public gridData: ClassRoom[] = [];
  public gridView: ClassRoom[] = [];
  public excelIcon: SVGIcon = fileExcelIcon;
  public pdfIcon: SVGIcon = filePdfIcon;

  public mySelection: string[] = [];

  constructor(private classRoomService: ClassRoomService, private router: Router) { }

  
  ngOnInit() {
      this.getClassRooms();
    }

    getClassRooms() {
      this.classRoomService.getClassRooms().subscribe(cr => {
        this.gridData = cr;
        this.gridView = cr;
      });
    }
  
  public getField = (args: ClassRoom) => {
      return `${args.name}_${args.building}_${args.capacity}_${args.number}_${args.type}`;
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

  public createClassRoom(){
      this.router.navigate(["class-rooms/new"])
  }
}
