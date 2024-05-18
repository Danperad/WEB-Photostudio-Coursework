import {EmployeeService, Service, Status} from "@models/*";
import {Button, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent, Stack, Typography} from "@mui/material";
import {useEffect, useState} from "react";
import {StatusType, StatusValue} from "../../models/Status.ts";
import {updateServicesStatus} from "../../services/employeeService.ts";

type OrderServiceInfoProps = {
  orderService: EmployeeService,
  onClose: () => void
}

function OrderServiceInfo(props: OrderServiceInfoProps) {
  const {orderService, onClose} = props
  const [allowedStatuses, setAllowedStatuses] = useState<Status[]>([])
  const [selectedStatus, setSelectedStatus] = useState<Status>(orderService.status as Status)

  useEffect(() => {
    const statuses: Status[] = []
    switch (orderService.status?.id) {
      case StatusValue.Done:
      case StatusValue.Canceled:
      case StatusValue.NotAccepted:
        return
      case StatusValue.NotStarted:
        statuses.push({id: StatusValue.InWork, type: StatusType.Service, title: "В работе"})
        statuses.push({id: StatusValue.Done, type: StatusType.Service, title: "Готова"})
        break
      case StatusValue.InWork:
        statuses.push({id: StatusValue.Done, type: StatusType.Service, title: "Готова"})
        break
    }
    setAllowedStatuses(statuses)
  }, [orderService]);

  const handleSelectStatus = (event: SelectChangeEvent<number>) => {
    if ((orderService.status as Status).id === +event.target.value)
      setSelectedStatus(orderService.status as Status)
    else
      setSelectedStatus(allowedStatuses[allowedStatuses.findIndex(s => s.id === +event.target.value)])
  }

  const handleStatusChange = () => {
    updateServicesStatus(orderService.id!, selectedStatus.id).then(res => {
      if (!res.ok){
        console.log(res.val)
      }
    }).catch(err => console.log(err))
  };

  return (
    <Stack direction={"column"}>
      <Typography onClick={onClose}>{(orderService.service as Service).title}</Typography>
      {allowedStatuses.length !== 0 && (
        <Stack direction={"row"}>
          <FormControl>
            <InputLabel id="status-select-label">Статус</InputLabel>
            <Select value={selectedStatus.id} onChange={handleSelectStatus} label={"Статус"}
                    labelId={"status-select-label"}>
              <MenuItem value={(orderService.status as Status).id}>
                <em>{(orderService.status as Status).title}</em>
              </MenuItem>
              {allowedStatuses.map((status) => (
                <MenuItem key={status.id} value={status.id}>{status.title}</MenuItem>
              ))}
            </Select>
          </FormControl>
          <Button variant={"contained"} disabled={selectedStatus === orderService.status} onClick={handleStatusChange}>Изменить
            статус</Button>
        </Stack>
      )}
    </Stack>
  )
}

export type {OrderServiceInfoProps}
export {OrderServiceInfo}
