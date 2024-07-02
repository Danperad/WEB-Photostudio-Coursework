import {Employee, Hall, Item, Order, OrderService, Service, ServicePackage, Status} from "@models/*";
import {
  Button,
  FormControl,
  IconButton,
  InputLabel,
  List,
  ListItem,
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
import {getServicesByOrders, updateOrderStatus} from "../../services/orderService.ts";
import dayjs from "dayjs";

type OrderInfoProps = {
  order: Order,
  onOrderStatusChanged: () => void,
  close: () => void
}

function OrderInfo(props: OrderInfoProps) {
  const {order, onOrderStatusChanged, close} = props
  const [orderServices, setOrderServices] = useState<OrderService[]>([])
  const [allowedStatuses, setAllowedStatuses] = useState<Status[]>([])
  const [selectedStatus, setSelectedStatus] = useState<Status>(order.status!)

  useEffect(() => {
    getServicesByOrders(order).then(res => {
      if (res.ok) {
        setOrderServices(res.val)
        setSelectedStatus(order.status!)
        setOrderStatusesRender()
      } else {
        console.log(res.val)
      }
    })
  }, [order]);

  const getEmployeeFullName = (employee: Employee) => {
    let name = employee.lastName + ` ` + employee.firstName.charAt(0) + `.`
    if (employee.middleName)
      name += ` ` + employee.middleName.charAt(0) + `.`
    return name
  }

  const setOrderStatusesRender = () => {
    const statuses: Status[] = []
    switch (order.status?.id) {
      case StatusValue.Done:
      case StatusValue.Canceled:
        return
      case StatusValue.NotAccepted:
        statuses.push({
          id: StatusValue.InWork,
          type: StatusType.Order,
          title: "В работе"
        })
        statuses.push({
          id: StatusValue.Canceled,
          type: StatusType.Order,
          title: "Отменена"
        })
        break
      case StatusValue.InWork:
        statuses.push({
          id: StatusValue.Canceled,
          type: StatusType.Order,
          title: "Отменена"
        })
        break;
    }
    setAllowedStatuses(statuses)
  }

  const handleSelectStatus = (event: SelectChangeEvent<number>) => {
    if (order.status!.id === +event.target.value)
      setSelectedStatus(order.status!)
    else
      setSelectedStatus(allowedStatuses[allowedStatuses.findIndex(s => s.id === +event.target.value)])
  }

  const handleStatusChange = () => {
    updateOrderStatus(order, selectedStatus).then(res => {
      if (res.ok) {
        onOrderStatusChanged()
        setOrderStatusesRender()
      } else {
        console.log(res.val)
      }
    }).catch(err => console.log(err))
  }

  return (
    <Stack>
      <Stack direction={"row"} width={"100%"} sx={{alignItems: "center"}} spacing={2}>
        <IconButton onClick={close}>
          <ArrowBack/>
        </IconButton>
        <Typography variant={"h6"}>Заявка №{order.number}</Typography>
      </Stack>
      <List>
        {order.servicePackage && (
          <Paper sx={{p: 2}}>
            <Typography>Пакет услуг</Typography>
            <Typography>{(order.servicePackage.servicePackage as ServicePackage).title}</Typography>
            <Typography>Начало: {dayjs(order.servicePackage.startDateTime).format("DD-MM-YY HH:mm")}</Typography>
            <Typography>Длительность: {(order.servicePackage.servicePackage as ServicePackage).duration} мин.</Typography>
          </Paper>
        )}
        {orderServices.map((service: OrderService) => (
          <ListItem key={service.id}>
            <Paper sx={{p: 2, width: "100%"}}>
              <Typography variant={"h6"}>{(service.service as Service).title}</Typography>
              {service.employee && (
                <Typography>Сотрудник: {getEmployeeFullName(service.employee as Employee)}</Typography>
              )}
              {service.hall && (
                <Typography>Зал: {(service.hall as Hall).title}</Typography>
              )}
              {service.item && (
                <Typography>Предмет: {(service.item as Item).title}</Typography>
              )}
              {service.count && (
                <Typography>Количество: {service.count}</Typography>
              )}
              {service.startDateTime && (
                <Typography>Начало: {dayjs(service.startDateTime).format("DD-MM-YY HH:mm")}</Typography>
              )}
              {service.duration && (
                <Typography>Длительность: {service.duration} мин.</Typography>
              )}
              {service.isFullTime && (
                <Typography>На всё время</Typography>
              )}
            </Paper>
          </ListItem>
        ))}
      </List>
      {allowedStatuses.length !== 0 && (
        <Stack direction={"row"} spacing={2} ml={2}>
          <FormControl>
            <InputLabel id="status-select-label">Статус</InputLabel>
            <Select value={selectedStatus.id} onChange={handleSelectStatus} label={"Статус"}
                    labelId={"status-select-label"}>
              <MenuItem value={order.status!.id}>
                <em>{order.status!.title}</em>
              </MenuItem>
              {allowedStatuses.map((status: Status) => (
                <MenuItem key={status.id} value={status.id}>{status.title}</MenuItem>
              ))}
            </Select>
          </FormControl>
          <Button variant={"contained"} disabled={selectedStatus === order.status} onClick={handleStatusChange}>Изменить
            статус</Button>
        </Stack>
      )}
    </Stack>
  )
}

export type {OrderInfoProps}
export {OrderInfo}
