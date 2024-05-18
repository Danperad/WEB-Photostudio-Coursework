import {Order, OrderService, Service, Status} from "@models/*";
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

type OrderInfoProps = {
  order: Order,
  close: () => void
}

function OrderInfo(props: OrderInfoProps) {
  const {order, close} = props
  const [orderServices, setOrderServices] = useState<OrderService[]>([])
  const [allowedStatuses, setAllowedStatuses] = useState<Status[]>([])
  const [selectedStatus, setSelectedStatus] = useState<Status>(order.status!)

  useEffect(() => {
    getServicesByOrders(order).then(res => {
      if (res.ok){
        setOrderServices(res.val)
      }
      else {
        console.log(res.val)
      }
    })
    const statuses : Status[] = []
    switch (order.status?.id){
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
  }, [order]);

  const handleSelectStatus = (event: SelectChangeEvent<number>) => {
    if (order.status!.id === +event.target.value)
      setSelectedStatus(order.status!)
    else
      setSelectedStatus(allowedStatuses[allowedStatuses.findIndex(s => s.id === +event.target.value)])
  }

  const handleStatusChange = () => {
    updateOrderStatus(order, selectedStatus).then(res => {
      if (!res.ok){
        console.log(res.val)
      }
    }).catch(err => console.log(err))
  }

  return (
    <Stack>
      <Stack direction={"row"} width={"100%"}>
        <IconButton onClick={close}>
          <ArrowBack/>
        </IconButton>
        <Typography>Заявка №{order.number}</Typography>
      </Stack>
      <Typography>Услуги</Typography>
      <List>
        {order.servicePackage && (
          <Paper>
            <Typography>Пакет услуг</Typography>
            <ListItem>{order.servicePackage.servicePackage.title}</ListItem>
          </Paper>
        )}
        {orderServices.map((service: OrderService) => (
          <ListItem>{(service.service as Service).title}</ListItem>
        ))}
      </List>
      {allowedStatuses.length !== 0 && (
        <Stack direction={"row"}>
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