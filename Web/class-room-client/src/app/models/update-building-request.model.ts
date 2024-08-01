export class BuildingRequest {
    constructor(
        public name: string,
        public address: string,
        public floorsNumber: number,
        public description?: string,
    ) { }
}
