import { Component, OnDestroy, OnInit } from "@angular/core";
import { NavigationStart, Router } from "@angular/router";
import { MessageService } from "@progress/kendo-angular-l10n";
import {
  DrawerComponent,
  DrawerMode,
  DrawerSelectEvent,
} from "@progress/kendo-angular-layout";
import { inheritedIcon, homeIcon } from "@progress/kendo-svg-icons";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
})
export class AppComponent implements OnInit, OnDestroy {
  public selected = "Team";
  public items: Array<any> = [];
 
  public mode: DrawerMode = "push";
  public mini = true;

  constructor(private router: Router, public msgService: MessageService) {
    
  }

  ngOnInit() {
    // Update Drawer selected state when change router path
    this.router.events.subscribe((route: any) => {
      if (route instanceof NavigationStart) {
        this.items = this.drawerItems().map((item) => {
          if (item.path && item.path === route.url) {
            item.selected = true;
            return item;
          } else {
            item.selected = false;
            return item;
          }
        });
      }
    });

    this.setDrawerConfig();

    window.addEventListener("resize", () => {
      this.setDrawerConfig();
    });
  }

  ngOnDestroy() {
    window.removeEventListener("resize", () => {});
  }

  public setDrawerConfig() {
    const pageWidth = window.innerWidth;
    if (pageWidth <= 840) {
      this.mode = "overlay";
      this.mini = false;
    } else {
      this.mode = "push";
      this.mini = true;
    }
  }

  public drawerItems() {
    return [
      {
        text: 'Здание/корпус',
        svgIcon: homeIcon,
        path: "/buildings",
        selected: true,
      },
      {
        text: 'Помещение',
        svgIcon: inheritedIcon,
        path: "/class-rooms",
        selected: false,
      },
    ];
  }

  public toggleDrawer(drawer: DrawerComponent): void {
    drawer.toggle();
  }

  public onSelect(ev: DrawerSelectEvent): void {
    this.router.navigate([ev.item.path]);
    this.selected = ev.item.text;
  }
}
