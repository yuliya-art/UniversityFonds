import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { AppComponent } from "./app.component";
import { HeaderComponent } from "./components/header/header.component";

import {
  ExcelModule,
  GridModule,
  PDFModule,
} from "@progress/kendo-angular-grid";
import { LabelModule } from "@progress/kendo-angular-label";
import { LayoutModule } from "@progress/kendo-angular-layout";
import { SchedulerModule } from "@progress/kendo-angular-scheduler";
import { ButtonsModule } from "@progress/kendo-angular-buttons";
import { EditorModule } from "@progress/kendo-angular-editor";
import { FileSelectModule } from "@progress/kendo-angular-upload";
import { ChartsModule } from "@progress/kendo-angular-charts";
import { IntlModule } from "@progress/kendo-angular-intl";
import { DateInputsModule } from "@progress/kendo-angular-dateinputs";
import { InputsModule } from "@progress/kendo-angular-inputs";
import { DropDownsModule } from "@progress/kendo-angular-dropdowns";
import { NotificationModule } from "@progress/kendo-angular-notification";
import { IconsModule } from "@progress/kendo-angular-icons";
import { MessageService } from "@progress/kendo-angular-l10n";
import { BuildingsComponent } from "./components/buildings/buildings.component";
import { BuildingDetailsComponent } from "./components/building-details/building-details.component";
import { CreateBuildingComponent } from "./components/create-building/create-building.component";
import { ClassRoomsComponent } from "./components/class-rooms/class-rooms.component";
import { CreateClassRoomComponent } from "./components/create-class-room/create-class-room.component";
import { ClassRoomDetailsComponent } from "./components/class-room-details/class-room-details.component";

const drawerRoutes = [
  { path: "buildings", component: BuildingsComponent },
  { path: "buildings/new", component: CreateBuildingComponent },
  { path: "buildings/:id", component: BuildingDetailsComponent },
  { path: "class-rooms", component: ClassRoomsComponent },
  { path: "class-rooms/new", component: CreateClassRoomComponent },
  { path: "class-rooms/:id", component: ClassRoomDetailsComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    BuildingsComponent,
    BuildingDetailsComponent,
    CreateBuildingComponent,
    ClassRoomsComponent,
    CreateClassRoomComponent,
    ClassRoomDetailsComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    GridModule,
    PDFModule,
    ExcelModule,
    LabelModule,
    LayoutModule,
    SchedulerModule,
    ButtonsModule,
    EditorModule,
    FileSelectModule,
    HttpClientModule,
    ChartsModule,
    IntlModule,
    DateInputsModule,
    InputsModule,
    DropDownsModule,
    RouterModule.forRoot(drawerRoutes),
    NotificationModule,
    IconsModule,
  ],
  providers: [{ provide: MessageService, useClass: MessageService }],
  bootstrap: [AppComponent],
})
export class AppModule {}
