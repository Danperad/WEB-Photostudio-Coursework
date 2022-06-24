import {Employee, ServicePackage} from "./Models";

declare type NewServicePackageModel = {
    service: ServicePackage,
    startTime: number,
    employee: Employee,
}

export default NewServicePackageModel;