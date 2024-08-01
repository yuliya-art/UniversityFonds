import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BuildingService } from '../../services/building.service';
import { NotificationService } from '@progress/kendo-angular-notification';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SVGIcon, arrowLeftIcon } from '@progress/kendo-svg-icons';
import { BuildingEditFormModel } from '../../models/building-form.model';
import { BuildingRequest } from '../../models/update-building-request.model';

@Component({
  selector: 'app-create-building',
  templateUrl: './create-building.component.html',
  styleUrl: './create-building.component.scss'
})
export class CreateBuildingComponent {
  public formGroup: FormGroup = new FormGroup({});
  public arrowLeftIcon: SVGIcon = arrowLeftIcon;

  constructor(private buildingService: BuildingService, private notificationService: NotificationService, private router: Router) {
    this.setFormValues();
  }
  
  public initialValue: BuildingEditFormModel = {};

  public setFormValues() {
    this.formGroup = new FormGroup({
      name: new FormControl(this.initialValue?.name, [Validators.required]),
      description: new FormControl(this.initialValue?.description, []),
      address: new FormControl(this.initialValue?.address, [Validators.required]),
      floorsNumber: new FormControl(this.initialValue?.floorsNumber, [Validators.required, Validators.min(0)]),
      id: new FormControl(this.initialValue?.id)
    });
}
  ngOnInit() {
  
  }

  public cancelChanges(): void {
    this.setFormValues();
}

public saveChanges(): void {
  this.formGroup.markAllAsTouched();
  const values = this.formGroup.value;

  this.buildingService.createBuilding(new BuildingRequest(values.name, values.address, values.floorsNumber, values.description))
  .pipe().subscribe(data=> {
    this.notificationService.show({
      content: 'Здание успешно создано',
      animation: { type: 'slide', duration: 500 },
      position: { horizontal: 'right', vertical: 'bottom' },
      type: { style: 'success', icon: true },
      hideAfter: 2000
  });
    this.router.navigate(["/buildings"]);
  }
    
  );

  this.formGroup.markAsPristine();
}
}
