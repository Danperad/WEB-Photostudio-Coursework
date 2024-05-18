export enum StatusType {
  Order= 1,
  Service
}

export enum StatusValue {
  Canceled = 1,
  NotAccepted = 2,
  InWork = 3,
  Done = 4,
  NotStarted = 5
}

interface Status {
  id: StatusValue,
  type: StatusType,
  title: string,
  description?: string | undefined
}

export type {Status}
