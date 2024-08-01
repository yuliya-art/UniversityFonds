export class Building {
    constructor(
        public id: string,
        public name: string,
        public description: string | undefined | null,
        public address: string,
        public floorsNumber: number
    ) { }
 }