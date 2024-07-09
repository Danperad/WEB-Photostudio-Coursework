import {NewServiceProps} from "./newServiceProps.ts";
import {Button, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent, Stack, Typography} from "@mui/material";
import {useEffect, useState} from "react";
import {Hall, OrderService} from "@models/*";
import {getAvailableHalls} from "../../../services/hallService.ts";

function HallRentService(props: NewServiceProps) {
  const {service, startTime, duration, isAllowSelect, onComplete} = props
  const [halls, setHalls] = useState<Hall[]>([])
  const [selectedHall, setSelectedHall] = useState<Hall | undefined>(undefined)

  useEffect(() => {
    if (!isAllowSelect())
      return
    getAvailableHalls(startTime, duration).then(res => {
      if (res.ok) {
        setHalls(res.val)
      } else {
        console.log(res.val)
      }
    }).catch(err => console.log(err))
  }, [startTime, duration]);

  const handleSelectEmployee = (event: SelectChangeEvent) => {
    setSelectedHall(halls[halls.findIndex((val) => val.id === +event.target.value)])
  };

  const handleComplete = () => {
    const orderService: OrderService = {
      service: service,
      startDateTime: startTime,
      duration: duration,
      hall: selectedHall
    }
    onComplete(orderService)
  }

  return (
    <Stack direction={"column"} spacing={1}>
      <Typography variant={"body2"}>{service.description}</Typography>
      <Stack direction={"row"}>
        <FormControl fullWidth>
          <InputLabel id="hall-select-label">Залы</InputLabel>
          <Select value={selectedHall ? String(selectedHall.id) : ``} label={"Залы"}
                  labelId={"hall-select-label"} onChange={handleSelectEmployee} disabled={!isAllowSelect()}>
            <MenuItem value={``}>
              <em>None</em>
            </MenuItem>
            {halls.map((hall) => (
              <MenuItem key={hall.id} value={hall.id}>{hall.title} ({hall.pricePerHour} руб./ч.)</MenuItem>
            ))}
          </Select>
        </FormControl>
      </Stack>
      <Button variant={"contained"} onClick={handleComplete}
              disabled={!isAllowSelect() || !selectedHall}>Добавить</Button>
    </Stack>
  )
}

export {HallRentService}
