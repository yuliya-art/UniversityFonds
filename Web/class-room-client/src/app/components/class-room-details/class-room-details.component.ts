import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { arrowLeftIcon, SVGIcon } from "@progress/kendo-svg-icons";
import { ClassRoomBuilding } from "../../models/class-room-building.model";
import { ClassRoomType } from "../../models/class-room-type.model";
import { ClassRoomService } from "../../services/class-room.service";
import { NotificationService } from "@progress/kendo-angular-notification";
import { ActivatedRoute, Router } from "@angular/router";
import { ClassRoomEditFormModel } from "../../models/class-room-form.model";
import { ClassRoomRequest } from "../../models/class-room-request.model";

@Component({
  selector: "app-class-room-details",
  templateUrl: "./class-room-details.component.html",
  styleUrl: "./class-room-details.component.scss",
})
export class ClassRoomDetailsComponent implements OnInit {
  id: string | null | undefined;
  public formGroup: FormGroup = new FormGroup({});
  public arrowLeftIcon: SVGIcon = arrowLeftIcon;
  public buildings: ClassRoomBuilding[] = [];
  public classRoomTypes: ClassRoomType[] = [];

  constructor(
    private route: ActivatedRoute,
    private classRoomService: ClassRoomService,
    private notificationService: NotificationService,
    private router: Router
  ) {
    this.route.paramMap.subscribe((params) => {
      this.id = params.get("id");

      if (this.id) {
        this.classRoomService.getClassRoomById(this.id).subscribe((room) => {
          this.initialValue = {
            name: room.name,
            id: room.id,
            building: room.building,
            buildingId: room.buildingId,
            floor: room.floor,
            capacity: room.capacity,
            number: room.number,
            type: room.type,
            typeId: room.typeId,
          };

          this.setFormValues();
        });
      }
    });

    this.setFormValues();
    this.classRoomService
      .getBuildings()
      .subscribe((data) => (this.buildings = data));
    this.classRoomService
      .getClassRoomTypes()
      .subscribe((data) => (this.classRoomTypes = data));
  }

  public initialValue: ClassRoomEditFormModel = {};

  public setFormValues() {
    this.formGroup = new FormGroup({
      name: new FormControl(this.initialValue?.name, [Validators.required]),
      building: new FormControl(this.initialValue?.building, []),
      buildingId: new FormControl(this.initialValue?.buildingId, [
        Validators.required,
      ]),
      number: new FormControl(this.initialValue?.number, [
        Validators.required,
        Validators.min(0),
      ]),
      capacity: new FormControl(this.initialValue?.capacity, [
        Validators.required,
        Validators.min(0),
      ]),
      type: new FormControl(this.initialValue?.type),
      typeId: new FormControl(this.initialValue?.typeId, [Validators.required]),
      id: new FormControl(this.initialValue?.id),
      floor: new FormControl(this.initialValue?.floor, [Validators.required]),
    });

    this.formGroup.get("id")?.disable();
  }

  ngOnInit() {}

  public cancelChanges(): void {
    this.setFormValues();
  }

  public remove() {
    this.classRoomService
      .removeClassRoom(this.initialValue.id!)
      .subscribe((data) => {
        this.notificationService.show({
          content: "Помещение успешно удалено",
          animation: { type: "slide", duration: 500 },
          position: { horizontal: "right", vertical: "bottom" },
          type: { style: "success", icon: true },
          hideAfter: 2000,
        });

        this.router.navigate(["/class-rooms"]);
      });
  }

  public saveChanges(): void {
    this.formGroup.markAllAsTouched();
    const values = this.formGroup.value;

    this.classRoomService
      .updateClassRoom(
        this.initialValue.id!,
        new ClassRoomRequest(
          values.name,
          values.buildingId,
          values.capacity,
          values.number,
          values.typeId,
          values.floor
        )
      )
      .pipe()
      .subscribe((data) => {
        this.notificationService.show({
          content: "Помещение успешно обновлено",
          animation: { type: "slide", duration: 500 },
          position: { horizontal: "right", vertical: "bottom" },
          type: { style: "success", icon: true },
          hideAfter: 2000,
        });
      });

    this.formGroup.markAsPristine();
  }
}
