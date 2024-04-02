import React, {useState} from 'react';
import {Employee, Hall, NewService, RentedItem, Service} from "../models/Models";
import {
  Box,
  Button,
  Checkbox,
  FormControl,
  FormControlLabel,
  FormGroup,
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
import ICostable from "../models/ICostable";
import RentedItemService from "../services/RentedItemService";
import EmployeeService from "../services/EmployeeService";
import {useDispatch} from "react-redux";
import {AppDispatch} from "../redux/store";
import {cartActions} from "../redux/slices/cartSlice";

const style = {
  position: 'absolute' as const,
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: '60%',
  bgcolor: '#F0EDE8',
  border: '2px solid #776D61',
  borderRadius: '20px',
  boxShadow: 24,
  p: 4,
};

interface ServiceModalProps {
  open: boolean,
  handlerClose: () => void,
  service: Service | null
}

export default function AddServiceModal(props: ServiceModalProps) {
  const title = () => {
    switch (props.service!.serviceType) {
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
  const [dateTime, setDateTime] = useState<string>('');
  const [items, setItems] = useState<ICostable[]>([]);
  const [duration, setDuration] = useState<string>("30");
  const [isEnabled, setEnabled] = useState<boolean>(false);
  const [selectedItem, setSelected] = useState<string>('');
  const [count, setCount] = useState<number>(0);
  const [address, setAddress] = useState<string>('');
  const [number, setNumber] = useState<string>('');
  const [fullEnabled, setFullEnabled] = useState<boolean>(false);
  const [isFullTime, setIsFullTime] = useState<boolean>(false);

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (+event.target.value < 0 || isNaN(+event.target.value)) return;
    setDuration(event.target.value);
    if (dateTime === undefined || dateTime === '') return;
    check(+new Date(dateTime as string), +event.target.value)
  };

  const handleAddressChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setAddress(event.target.value);
    if (event.target.value.split(' ').length === 2 && event.target.value.split(' ')[1].length > 0) setFullEnabled(true);
    else setFullEnabled(false);
  }

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
      setItems([]);
      setSelected('');
      setAddress('');
      setNumber('');
      setFullEnabled(false);
      return;
    }
    if (props.service!.serviceType === 5) {
      setEnabled(true);
    }
    fillItems(date, dur);

  }
  const fillItems = (date: number, dur: number) => {
    switch (props.service!.id) {
      case 7:
        HallService.getFree(date, dur).then((res) => {
          if (res.length === 0) return;
          setItems(res);
          setEnabled(true);
        })
        break;
      case 5:
      case 6:
      case 10:
        RentedItemService.getFree(date, dur, props.service!.id).then((res) => {
          if (res.length === 0) return;
          setItems(res);
          setEnabled(true);
        });
        break;
      default:
        EmployeeService.getFree(date, dur, props.service!.id).then((res) => {
          if (res.length === 0) return;
          setItems(res);
          setEnabled(true);
        });
        break;
    }
  }

  const handleSelect = (event: SelectChangeEvent) => {
    setSelected(event.target.value as string);
    if (props.service!.serviceType === 4)
      setCount(getAvailable(event.target.value as string))
    if ((event.target.value as string) === '') {
      setFullEnabled(false)
      return;
    }
    if (props.service!.serviceType === 2) setFullEnabled(true)
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
    let item: ICostable | null = null;
    items.forEach((i) => {
      if (i.id === +selectedItem) item = i;
    });
    if (item !== null) {
      let itemPrice: number;
      if (props.service!.serviceType === 4 && number !== '') itemPrice = ((+number) * (item as ICostable)!.cost);
      else itemPrice = (item as ICostable)!.cost;
      price += itemPrice * ((+duration) / 60);
    }
    return price;
  }

  const buy = () => {
    const newService: NewService = {
      id: new Date().getTime() + Math.random(),
      service: props.service!,
      startTime: +new Date(dateTime),
      duration: +duration,
      hall: null,
      employee: null,
      address: null,
      rentedItem: null,
      number: null,
      isFullTime: null,
    }
    switch (props.service!.id) {
      case 7:
        let res: Hall | null = null;
        items.forEach((item) => {
          if (item.id === +selectedItem) res = (item as Hall);
        });
        newService.hall = res!;
        break;
      case 5:
      case 6:
      case 10:
        let rented: RentedItem | null = null;
        items.forEach((item) => {
          if (item.id === +selectedItem) rented = (item as RentedItem);
        });
        newService.rentedItem = rented!;
        newService.number = +number;
        break;
      case 13:
        newService.isFullTime = isFullTime;
        let empl1: Employee | null = null;
        items.forEach((item) => {
          if (item.id === +selectedItem) empl1 = item;
        });
        newService.employee = empl1!;
        newService.address = address;
        break;
      default:
        let empl: Employee | null = null;
        items.forEach((item) => {
          if (item.id === +selectedItem) empl = item;
        });
        newService.employee = empl!;
        newService.address = address;
        break;
    }
    dispatch(cartActions.ServiceAdded(newService));
    props.handlerClose();
  }

  if (props.service === null) return <></>;
  return (
    <Modal
      open={props.open}
      onClose={props.handlerClose}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Box sx={style}>
        <Stack direction="row" ml={16} spacing={2} width={"70%"} mt={2} justifyContent="space-between"
               alignItems="center">
          <Stack spacing={2}>
            <Typography variant="subtitle1">
              Название:
            </Typography>
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
            <TextField label="Период" color='primary' inputProps={{inputMode: 'numeric', pattern: '[0-9]*'}}
                       size='small' sx={{width: '100%'}}
                       value={duration} onChange={handleChange}/>
            {props.service.serviceType === 5 &&
              <FormGroup>
                <FormControlLabel control={<Checkbox checked={isFullTime} onChange={() => {
                  setIsFullTime(!isFullTime)
                }}/>} label="На все время?"/>
              </FormGroup>
            }
          </Stack>
          <Stack spacing={2}>
            <FormControl fullWidth>
              <InputLabel id="demo-simple-select-label"
                          style={{lineHeight: '0.7em', height: '20px'}}>{objectTitle}</InputLabel>
              <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                label={objectTitle}
                value={selectedItem}
                sx={{height: '40px'}} disabled={!isEnabled}
                onChange={handleSelect}
              >
                {items.map((hall, index) => (
                  <MenuItem key={index} value={hall.id}>{hall.title}</MenuItem>
                ))}
              </Select>
            </FormControl>
            {(props.service!.serviceType === 3 || props.service!.serviceType === 5) &&
              <TextField label={"Адрес"} color='primary' size='small' sx={{width: '100%'}}
                         disabled={!isEnabled} value={address} onChange={handleAddressChange}/>
            }
            {props.service!.serviceType === 4 &&
              <>
                <TextField label={"Количество"} inputProps={{inputMode: 'numeric', pattern: '[0-9]*'}}
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
                  Купить
                </Button>
              </Stack>
            </Stack>
          </Stack>
        </Stack>
      </Box>
    </Modal>
  )
}
