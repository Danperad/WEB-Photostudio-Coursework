import {Employee, Hall, RentedItem, Service} from "./Models";

declare type NewServiceModel = {
    id: number
    service: Service,
    startDateTime?: Date,
    duration?: number,
    hall?: Hall,
    employee?: Employee,
    rentedItem?: RentedItem,
    number?: number,
    isFullTime?: boolean,
}

export default NewServiceModel;