enum StatusType {
    Order = 1,
    Service
}

enum StatusValue {
    Canceled = 1,
    NotAccepted = 2,
    InWork = 3,
    Done = 4,
    NotStarted = 5
}

type Status = {
    id: StatusValue,
    type: StatusType,
    name: string
}

export type {Status}
export {StatusType, StatusValue}