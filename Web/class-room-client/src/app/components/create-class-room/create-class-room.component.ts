import { Component } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { NotificationService } from "@progress/kendo-angular-notification";
import { arrowLeftIcon, SVGIcon } from "@progress/kendo-svg-icons";
import { ClassRoomService } from "../../services/class-room.service";
import { ClassRoomEditFormModel } from "../../models/class-room-form.model";
import { ClassRoomRequest } from "../../models/class-room-request.model";
import { ClassRoomBuilding } from "../../models/class-room-building.model";
import { ClassRoomType } from "../../models/class-room-type.model";

@Component({
  selector: "app-create-class-room",
  templateUrl: "./create-class-room.component.html",
  styleUrl: "./create-class-room.component.scss",
})
export class CreateClassRoomComponent {
  public formGroup: FormGroup = new FormGroup({});
  public arrowLeftIcon: SVGIcon = arrowLeftIcon;
  public buildings: ClassRoomBuilding[] = [];
  public classRoomTypes: ClassRoomType[] = [];

  constructor(
    private classRoomService: ClassRoomService,
    private notificationService: NotificationService,
    private router: Router
  ) {
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
      typeId: new FormControl(this.initialValue?.type, [Validators.required]),
      id: new FormControl(this.initialValue?.id),
      floor: new FormControl(this.initialValue?.floor, [Validators.required]),
    });
  }

  ngOnInit() {}

  public cancelChanges(): void {
    this.setFormValues();
  }

  public saveChanges(): void {
    this.formGroup.markAllAsTouched();
    const values = this.formGroup.value;
    this.classRoomService
      .createClassRoom(
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
          content: "Помещение успешно создано",
          animation: { type: "slide", duration: 500 },
          position: { horizontal: "right", vertical: "bottom" },
          type: { style: "success", icon: true },
          hideAfter: 2000,
        });
        this.router.navigate(["/class-rooms"]);
      });

    this.formGroup.markAsPristine();
  }
}
