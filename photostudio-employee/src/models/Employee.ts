import {Service} from "./Service.ts";

interface Employee {
  id: number,
  lastName: string,
  firstName: string,
  middleName: string | undefined,
  cost: number,
}

interface Role {
  id: number,
  title: string
}

interface EmployeeWithRole extends Employee {
  role: Role,
  boundServices: Service[]
}

export type {Employee, Role, EmployeeWithRole}
