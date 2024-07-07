import {EmployeeService, Hall, Item, Service, Status} from "@models/*";
import {
  Button, Container,
  FormControl,
  IconButton,
  InputLabel,
  MenuItem,
  Paper,
  Select,
  SelectChangeEvent,
  Stack,
  Typography
} from "@mui/material";
import {ArrowBack} from "@mui/icons-material";
import {useEffect, useState} from "react";
import {StatusType, StatusValue} from "../../models/Status.ts";
import {updateServicesStatus} from "../../services/employeeService.ts";
import dayjs from "dayjs";

type OrderServiceInfoProps = {
  orderService: EmployeeService,
  onClose: () => void,
  onOrderServiceStatusChange: () => void
}

function OrderServiceInfo(props: OrderServiceInfoProps) {
  const {orderService, onClose, onOrderServiceStatusChange} = props
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
      } else {
        onOrderServiceStatusChange()
      }
    }).catch(err => console.log(err))
  };

  return (
    <Stack direction={"column"} spacing={1} px={1}>
      <Container>
        <IconButton onClick={onClose}>
          <ArrowBack/>
        </IconButton>
      </Container>
      <Paper sx={{mx: 1, p: 1}}>
        <Typography>{(orderService.service as Service).title}</Typography>
        {orderService.hall && (
          <Typography>Зал: {(orderService.hall as Hall).title}</Typography>
        )}
        {orderService.item && (
          <Typography>Предмет: {(orderService.item as Item).title}</Typography>
        )}
        {orderService.count && (
          <Typography>Количество: {orderService.count}</Typography>
        )}
        {orderService.startDateTime && (
          <Typography>Начало: {dayjs(orderService.startDateTime).format("DD-MM-YY HH:mm")}</Typography>
        )}
        {orderService.duration && (
          <Typography>Длительность: {orderService.duration} мин.</Typography>
        )}
        {orderService.isFullTime && (
          <Typography>На всё время</Typography>
        )}
      </Paper>
      {allowedStatuses.length !== 0 && (
        <Stack direction={"row"} spacing={2} ml={2}>
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
