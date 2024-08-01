import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SVGIcon, menuIcon } from '@progress/kendo-svg-icons';

@Component({
    selector: 'app-header-component',
    templateUrl: './header.commponent.html'
})
export class HeaderComponent {
    @Output() public toggle = new EventEmitter();
    @Input() public selectedPage?: string;

    public menuIcon: SVGIcon = menuIcon;
 
    public onButtonClick(): void {
        this.toggle.emit();
    }
}
