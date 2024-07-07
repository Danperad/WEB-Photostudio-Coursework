import React, {useEffect, useState} from 'react';
import {Employee, Hall, NewService, RentedItem, Service} from "../models/Models";
import {
  Box,
  Button,
  Checkbox,
  FormControl,
  FormControlLabel,
  FormGroup,
  Grid,
  InputLabel,
  MenuItem,
  Modal,
  Select,
  SelectChangeEvent,
  Stack,
  TextField,
  Typography
} from "@mui/material";
import HallService from "../services/HallService";
import RentedItemService from "../services/RentedItemService";
import EmployeeService from "../services/EmployeeService";
import {useDispatch} from "react-redux";
import {AppDispatch} from "../redux/store";
import {cartActions} from "../redux/slices/cartSlice";
import {ServiceType} from "../models/ServiceModel.ts";
import {format} from "date-fns";

const style = {
  position: 'absolute' as const,
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: '45%',
  bgcolor: '#F0EDE8',
  border: '2px solid #776D61',
  borderRadius: '20px',
  boxShadow: 24,
  p: 4,
};

interface ServiceModalProps {
  open: boolean,
  handlerClose: (res: boolean) => void,
  service: Service | null
}

export default function AddServiceModal(props: ServiceModalProps) {
  const title = () => {
    switch (props.service!.type) {
      case 2:
        return "Зал";
      case 3:
        return "Сотрудник";
      case 4:
        return "Объект";
      default:
        return "Сотрудник";
    }
  }
  const dispatch = useDispatch<AppDispatch>();

  const [objectTitle] = useState<string>(title());
  const [dateTime, setDateTime] = useState<string>(format(new Date(new Date().setDate(new Date().getDate())), "dd-MM-yyyyTHH:mm"));
  const [halls, setHalls] = useState<Hall[]>([])
  const [items, setItems] = useState<RentedItem[]>([])
  const [employees, setEmployees] = useState<Employee[]>([])
  const [duration, setDuration] = useState<string>("60");
  const [isEnabled, setEnabled] = useState<boolean>(false);
  const [selectedItem, setSelected] = useState<string>('');
  const [count, setCount] = useState<number>(0);
  const [number, setNumber] = useState<string>('');
  const [fullEnabled, setFullEnabled] = useState<boolean>(false);
  const [isFullTime, setIsFullTime] = useState<boolean>(false);
  const [key, setKey] = useState<boolean>(false);

  useEffect(() => {
    if (!props.open) {
      setKey(false);
      return;
    }
    if (key) return;
    setKey(true);
  }, [props])

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (+event.target.value < 0 || isNaN(+event.target.value)) return;
    setDuration(event.target.value);
    if (dateTime === undefined || dateTime === '') return;
    check(+new Date(dateTime as string), +event.target.value)
  };

  const handleNumberChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.value === '') {
      setNumber(event.target.value);
      setFullEnabled(false);
      return;
    }
    if (+event.target.value < 0 || isNaN(+event.target.value) || +event.target.value > count) {
      return;
    }
    if (+event.target.value === 0) {
      setNumber(event.target.value);
      setFullEnabled(false);
      return;
    }
    setFullEnabled(true);
    setNumber(event.target.value);
  }
  const check = (date: number, dur: number) => {
    if (date < +Date.now() || dur < 60) {
      setEnabled(false);
      setHalls([])
      setItems([]);
      setEmployees([])
      setSelected('');
      setNumber('');
      setFullEnabled(false);
      return;
    }
    if (props.service!.type === 1) {
      setEnabled(true);
    }
    fillItems(date, dur);

  }
  const fillItems = (date: number, dur: number) => {
    switch (props.service!.type) {
      case ServiceType.HallRent:
        HallService.getFree(new Date(date), dur).then((res) => {
          if (res.length === 0) return;
          setHalls(res);
          setEnabled(true);
        }).catch(err => {
          console.log(err);
        })
        break;
      case ServiceType.ItemRent:
        RentedItemService.getFree(date, dur, 1).then((res) => {
          if (res.length === 0) return;
          setItems(res);
          setEnabled(true);
        });
        break;
      default:
        EmployeeService.getFree(date, dur, props.service!.id).then((res) => {
          if (res.length === 0) return;
          setEmployees(res);
          setEnabled(true);
        });
        break;
    }
  }

  const handleSelect = (event: SelectChangeEvent) => {
    setSelected(event.target.value as string);
    if (props.service!.type === 4)
      setCount(getAvailable(event.target.value as string))
    if ((event.target.value as string) === '') {
      setFullEnabled(false)
      return;
    }
    setFullEnabled(true)
  };

  const getAvailable = (itemm: string) => {
    let res = 0;
    if (itemm === '') return res;
    items.forEach((item) => {
      if (item.id === +itemm)
        res = (item as RentedItem).number;
    });
    return res;
  }

  const getPrice = () => {
    let price = props.service!.cost;
    switch (props.service?.type) {
      case ServiceType.ItemRent:
        items.forEach((item) => {
          if (item.id === +selectedItem) {
            let itemPrice: number = 0;
            if (number !== '') itemPrice = ((+number) * item!.cost) * (+duration) / 60;
            price += itemPrice;
          }
        });
        break
      case ServiceType.HallRent:
        halls.forEach((hall) => {
          if (hall.id === +selectedItem) {
            price += hall.pricePerHour * (+duration) / 60
          }
        });
        break
      case ServiceType.Photo:
      case ServiceType.Style:
        employees.forEach((empl) => {
          if (empl.id === +selectedItem) {
            const multipl = isFullTime ? 1.5 : 1;
            price += (empl.cost * (+duration) / 60) * multipl
          }
        });
        break
    }
    return Math.round(price);
  }

  const buy = () => {
    const newService: NewService = {
      id: new Date().getTime() + Math.random(),
      service: props.service!,
      startDateTime: new Date(dateTime),
      duration: +duration,
    }
    switch (props.service!.type) {
      case ServiceType.HallRent:
        halls.forEach((item) => {
          if (item.id === +selectedItem) {
            newService.hall = item;
          }
        });
        break;
      case ServiceType.ItemRent:
        items.forEach((item) => {
          if (item.id === +selectedItem) {
            newService.rentedItem = item!;
          }
        });
        newService.number = +number;
        break;
      case ServiceType.Style:
        employees.forEach((item) => {
          if (item.id === +selectedItem) newService.employee = item;
        });
        newService.isFullTime = isFullTime;
        break;
      default:
        employees.forEach((item) => {
          if (item.id === +selectedItem) newService.employee = item;
        });
        break;
    }
    dispatch(cartActions.ServiceAdded(newService));
    props.handlerClose(true);
  }

  if (props.service === null) return <></>;
  return (
    <Modal
      open={props.open}
      onClose={() => {
        props.handlerClose(false)
      }}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Box sx={style}>
        <Grid container spacing={2} columns={4} ml={16} width={"70%"} justifyContent="space-between"
              alignItems="center">
          <Grid item xs={4}>
            <Typography variant="subtitle1">
              Название: {props.service.title}
            </Typography>
          </Grid>
          <Stack direction={"row"} spacing={2}>
            <Grid item xs={2}>
              <Stack spacing={2}>
                <TextField
                  id="datetime-local"
                  label="Дата и время"
                  type="datetime-local"
                  defaultValue={dateTime}
                  onChange={e => {
                    setDateTime(e.target.value);
                    check(+new Date(e.target.value), +duration)
                  }}
                  InputLabelProps={{
                    shrink: true,
                  }}
                />
                <TextField label="Период" color='primary'
                           inputProps={{inputMode: 'numeric', pattern: '[0-9]*'}}
                           size='small' sx={{width: '100%'}}
                           value={duration} onChange={handleChange}/>
                {props.service.type === ServiceType.Style &&
                  <FormGroup>
                    <FormControlLabel control={<Checkbox checked={isFullTime} onChange={() => {
                      setIsFullTime(!isFullTime)
                    }}/>} label="На все время?"/>
                  </FormGroup>
                }
              </Stack>
            </Grid>
            <Grid item xs={2}>
              <Stack spacing={2}>
                <FormControl fullWidth>
                  <InputLabel id="demo-simple-select-label"
                              style={{lineHeight: '0.7em', height: '20px'}}>{objectTitle}</InputLabel>
                  <Select
                    labelId="demo-simple-select-label"
                    id="demo-simple-select"
                    label={objectTitle}
                    value={selectedItem}
                    sx={{height: '55px'}} disabled={!isEnabled}
                    onChange={handleSelect}
                  >
                    {props.service.type === ServiceType.ItemRent && items.map((item, index) => (
                      <MenuItem key={index}
                                value={item.id}>{item.title} ({item.cost} руб./ч)</MenuItem>
                    ))}
                    {props.service.type === ServiceType.HallRent && halls.map((hall, index) => (
                      <MenuItem key={index}
                                value={hall.id}>{hall.title} ({hall.pricePerHour} руб./ч)</MenuItem>
                    ))}
                    {(props.service.type === ServiceType.Style || props.service.type === ServiceType.Photo) && employees.map((empl, index) => (
                      <MenuItem key={index}
                                value={empl.id}>{empl.lastName} {empl.firstName} {empl.middleName} ({empl.cost} руб./ч)</MenuItem>
                    ))}
                  </Select>
                </FormControl>
                {props.service!.type === ServiceType.ItemRent &&
                  <>
                    <TextField label={"Количество"}
                               inputProps={{inputMode: 'numeric', pattern: '[0-9]*'}}
                               color='primary' size='small' sx={{width: '100%'}}
                               disabled={!isEnabled} value={number} onChange={handleNumberChange}/>
                    <Typography>Доступно: {count}</Typography>
                  </>
                }
                <Stack direction="row" width={"80%"} justifyContent="space-between" alignItems="center">
                  <div style={{width: "100px"}}></div>
                  <Stack direction="row" spacing={2}>
                    <Typography variant="subtitle1" style={{whiteSpace: "nowrap"}}>
                      Стоимость: {getPrice()} рублей
                    </Typography>
                    <Button variant="contained" color="secondary" size="medium" disableElevation
                            sx={{borderRadius: '10px'}}
                            onClick={() => {
                              buy()
                            }} disabled={!fullEnabled}>
                      Добавить
                    </Button>
                  </Stack>
                </Stack>
              </Stack>
            </Grid>
          </Stack>
        </Grid>
      </Box>
    </Modal>
  )
}
