import {NewServiceProps} from "./newServiceProps.ts";
import {
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
  Stack,
  TextField,
  Typography
} from "@mui/material";
import {ChangeEvent, useEffect, useState} from "react";
import {Item, OrderService} from "@models/*";
import {getAvailableItems} from "../../../services/rentedItemService.ts";

function ItemRentService(props: NewServiceProps) {
  const {service, startTime, duration,isAllowSelect, onComplete} = props
  const [items, setItems] = useState<Item[]>([])

  const [selectedCount, setSelectedCount] = useState<number>(0)
  const [selectedItem, setSelectedItem] = useState<Item | undefined>(undefined)

  useEffect(() => {
    if (!isAllowSelect())
      return
    getAvailableItems(startTime, duration).then(res => {
      if (res.ok){
        setItems(res.val)
      }
      else {
        console.log(res.val)
      }
    }).catch(err => console.log(err))
  }, [startTime, duration]);

  const countSelectHandler = (event: ChangeEvent<HTMLInputElement>) => {
    let count = +event.target.value
    if (count < 0)
      count = 0
    if (count > 20)
      count = 300
    setSelectedCount(count)
  }

  const handleSelectItem = (event: SelectChangeEvent) => {
    setSelectedItem(items[items.findIndex((val) => val.id === +event.target.value)])
  };

  const handleComplete = () => {
    const orderService: OrderService = {
      service: service,
      startDateTime: startTime,
      duration: duration,
      item: selectedItem,
      count: selectedCount,
    }
    onComplete(orderService)
  }

  return (
    <Stack direction={"column"}>
      <Typography variant={"body2"}>{service.description}</Typography>
      <Stack direction={"row"}>
        <TextField label={"Количество"} inputProps={{inputMode: "numeric", pattern: "[0-9]{1,4}"}}
                   value={selectedCount} onChange={countSelectHandler} sx={{width: "10vw"}}/>
      </Stack>
      <Stack direction={"row"}>
        <FormControl fullWidth>
          <InputLabel id="item-select-label">Предметы</InputLabel>
          <Select value={selectedItem ? String(selectedItem.id) : ``} label={"Сотрудники"}
                  labelId={"item-select-label"} onChange={handleSelectItem} disabled={!isAllowSelect()}>
            <MenuItem value={``}>
              <em>None</em>
            </MenuItem>
            {items.map((item) => (
              <MenuItem key={item.id} value={item.id}>{item.title}</MenuItem>
            ))}
          </Select>
        </FormControl>
      </Stack>
      <Button variant={"contained"} onClick={handleComplete}
              disabled={!isAllowSelect() || !selectedItem || !selectedCount}>Добавить</Button>
    </Stack>
  )
}

export {ItemRentService}
