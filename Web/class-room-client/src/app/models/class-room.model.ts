export class ClassRoom {
    constructor(
        public id: string,
        public name: string,
        public building: string | undefined,
        public buildingId: string | undefined,
        public capacity: number,
        public number: number,
        public type: string,
        public typeId: number,
        public floor: number
    ) { }
 }