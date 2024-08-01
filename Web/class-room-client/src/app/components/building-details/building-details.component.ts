import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BuildingEditFormModel } from '../../models/building-form.model';
import { BuildingService } from '../../services/building.service';
import { SVGIcon, arrowLeftIcon } from '@progress/kendo-svg-icons';
import { BuildingRequest } from '../../models/update-building-request.model';
import { NotificationService } from '@progress/kendo-angular-notification';

@Component({
  selector: 'app-building-details',
  templateUrl: './building-details.component.html',
  styleUrl: './building-details.component.scss'
})

export class BuildingDetailsComponent  implements OnInit {
  id: string | null | undefined;

  public formGroup: FormGroup = new FormGroup({});
  public arrowLeftIcon: SVGIcon = arrowLeftIcon;

  constructor(private route: ActivatedRoute, private buildingService: BuildingService, private notificationService: NotificationService, private router: Router) {

    this.route.paramMap.subscribe(params => {
      this.id = params.get('id')

      if(this.id){
        this.buildingService.getBuildingById(this.id).subscribe(building => {
          this.initialValue = {
             name: building.name,
             description: building.description ?? undefined,
             address: building.address,
             floorsNumber: building.floorsNumber,
             id: building.id
          }
  
          this.setFormValues()
        });
      }
    });

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

    this.formGroup.get('id')?.disable()
}

  ngOnInit() {
   
  }

  public cancelChanges(): void {
    this.setFormValues();
}

public saveChanges(): void {
  this.formGroup.markAllAsTouched();
  const values = this.formGroup.value;

  this.buildingService.updateBuilding(this.initialValue.id!, new BuildingRequest(values.name, values.address, values.floorsNumber, values.description))
  .subscribe(building => {
    this.initialValue = {
       name: building.name,
       description: building.description ?? undefined,
       address: building.address,
       floorsNumber: building.floorsNumber,
       id: building.id
    }

    this.setFormValues()
  });

  this.formGroup.markAsPristine();

  this.notificationService.show({
      content: 'Здание успешно обновлено',
      animation: { type: 'slide', duration: 500 },
      position: { horizontal: 'right', vertical: 'bottom' },
      type: { style: 'success', icon: true },
      hideAfter: 2000
  });
}

public removeBuilding(){
  this.buildingService.removeBuilding(this.initialValue.id!)
  .subscribe(data=>{
    this.notificationService.show({
      content: 'Здание успешно удалено',
      animation: { type: 'slide', duration: 500 },
      position: { horizontal: 'right', vertical: 'bottom' },
      type: { style: 'success', icon: true },
      hideAfter: 2000
  });
  
  this.router.navigate(["/buildings"]);
  });
}
}
