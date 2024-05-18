import {ChangeEvent, useEffect, useState} from "react";
import {Client, Order, OrderService, OrderServicePackage, Service, ServicePackage} from "@models/*";
import {ArrowBack} from '@mui/icons-material';
import {
  Button,
  Divider,
  FormControl,
  IconButton,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
  Stack,
  TextField,
  Typography
} from "@mui/material";
import {ServiceType} from "../../models/Service.ts"
import {HallRentService, ItemRentService, PhotoService, SimpleService, StyleService} from "./newService";
import {NewOrderServicePackage} from "./newService/NewOrderServicePackage.tsx";
import {DatePicker, TimePicker} from "@mui/x-date-pickers";
import dayjs, {Dayjs} from "dayjs";
import {getServices} from "../../services/serviceService.ts";
import {addNewOrder} from "../../services/orderService.ts";

const startTime = dayjs().set('hour', 8).startOf('hour');
const endTime = dayjs().set('hour', 22).startOf('hour');

const packages: ServicePackage[] = [
  {id: 1, title: "Ну чтоб прям вообще", description: "Это описание", cost: 50000}
]

type NewOrderTabProps = {
  client: Client,
  close: () => void
}

function NewOrderTab(props: NewOrderTabProps) {
  const {client, close} = props
  const [availableServices, setAvailableServices] = useState<Service[]>([])

  const [selectedService, setSelectedService] = useState<Service | undefined>(undefined)
  const [selectedPackage, setSelectedPackage] = useState<ServicePackage | undefined>(undefined)
  const [addedServices, setAddedServices] = useState<OrderService[]>([])
  const [addedPackage, setAddedPackage] = useState<OrderServicePackage | undefined>(undefined)

  const [selectedDate, setSelectedDate] = useState<Dayjs>(dayjs());
  const [selectedTime, setSelectedTime] = useState<Dayjs>(dayjs().set("hour", 8).set("minute", 0));
  const [selectedDuration, setSelectedDuration] = useState<number>(60);


  useEffect(() => {
    getServices().then(res => {
      if (res.ok){
        setAvailableServices(res.val)
      }
      else {
        console.log(res.val)
      }
    }).catch(err => console.log(err))
  }, [client]);

  const getStartTime = (): Date => {
    return selectedDate.set("hour", selectedTime.hour()).set("minute", selectedTime.minute()).toDate()
  }

  const fullName = () => {
    let name = client.lastName + ` ` + client.firstName.charAt(0) + `.`
    if (client.middleName)
      name += ` ` + client.middleName.charAt(0) + `.`
    return name
  }

  const dateSelectHandler = (date: Dayjs | null) => {
    if (date === null)
      return
    setSelectedDate(date)
  }

  const timeSelectHandler = (date: Dayjs | null) => {
    if (date === null)
      return
    setSelectedTime(date)
  }

  const durationSelectHandler = (event: ChangeEvent<HTMLInputElement>) => {
    let duration = +event.target.value
    if (duration < 0)
      duration = 0
    if (duration > 300)
      duration = 300
    setSelectedDuration(duration)
  }

  const isAllowSelect = (): boolean => {
    const selected = selectedDate.set("hour", selectedTime.hour()).set("minute", selectedTime.minute())
    if (selected.isBefore(dayjs().add(1, "day")))
      return false
    return selectedDuration >= 60;
  }

  const isAllowAddPackage = (): boolean => {
    const selected = selectedDate.set("hour", selectedTime.hour()).set("minute", selectedTime.minute())
    return !selected.isBefore(dayjs().add(1, "day"));

  }

  const handleSelectService = (event: SelectChangeEvent) => {
    setSelectedPackage(undefined)
    setSelectedService(availableServices[availableServices.findIndex((val) => val.id === +event.target.value)])
  };

  const handleSelectPackage = (event: SelectChangeEvent) => {
    setSelectedService(undefined)
    setSelectedPackage(packages[packages.findIndex((val) => val.id === +event.target.value)])
  };

  const addServiceHandler = (service: OrderService) => {
    setAddedServices(prevState => {
      prevState.push(service)
      return prevState
    })
    setSelectedService(undefined)
  }

  const addPackageHandler = (servicePackage: OrderServicePackage) => {
    setAddedPackage(servicePackage)
    setSelectedPackage(undefined)
  }

  const handleBuy = () => {
    const order: Order = {
      client: client.id!,
      services: addedServices
    }
    console.log(order)
    addNewOrder(order).then(res => {
      if (res.ok){
        close()
      }
      else {
        console.log(res.val)
      }
    })
  }

  return (
    <Stack width={"50%"} direction={"column"}>
      <Stack direction={"row"} width={"100%"}>
        <IconButton onClick={close}>
          <ArrowBack/>
        </IconButton>
        <Typography>Новая заявка для {fullName()}</Typography>
      </Stack>
      <Stack direction={"row"}>
        <DatePicker label={"Дата"} format={"DD-MM-YYYY"} defaultValue={dayjs().add(1, "day")} value={selectedDate}
                    onChange={dateSelectHandler} sx={{width: "8vw"}} disablePast/>
        <TimePicker label={"Время"} ampm={false} value={selectedTime} onChange={timeSelectHandler} sx={{width: "6vw"}}
                    minTime={startTime} maxTime={endTime}/>
        <TextField label={"Длительность (мин.)"} inputProps={{inputMode: "numeric", pattern: "[0-9]{1,4}"}}
                   value={selectedDuration} onChange={durationSelectHandler} sx={{width: "10vw"}}/>
      </Stack>
      <Stack direction={"row"} width={"100%"}>
        <FormControl fullWidth>
          <InputLabel id="service-select-label">Услуги</InputLabel>
          <Select value={selectedService ? String(selectedService.id) : ``} labelId={"service-select-label"}
                  label={"Услуги"}
                  onChange={handleSelectService}>
            <MenuItem value={``}>
              <em>None</em>
            </MenuItem>
            {availableServices.map((service) => (
              <MenuItem key={service.id} value={service.id}>{service.title}</MenuItem>
            ))}
          </Select>
        </FormControl>
        <FormControl fullWidth>
          <InputLabel id="service-package-select-label">Пакеты услуг</InputLabel>
          <Select value={selectedPackage ? String(selectedPackage.id) : ``} labelId={"service-package-select-label"}
                  label={"Пакеты услуг"}
                  onChange={handleSelectPackage} disabled={addedPackage !== undefined}>
            <MenuItem value={``}>
              <em>None</em>
            </MenuItem>
            {packages.map((servicePackage) => (
              <MenuItem key={servicePackage.id} value={servicePackage.id}>{servicePackage.title}</MenuItem>
            ))}
          </Select>
        </FormControl>

      </Stack>
      <Divider variant={"middle"} orientation={"horizontal"}/>
      {(selectedPackage || selectedService) && (
        <>
          {selectedPackage && (
            <NewOrderServicePackage servicePackage={selectedPackage} startTime={getStartTime()}
                                    isAllowSelect={isAllowAddPackage} onComplete={addPackageHandler}/>
          )}
          {selectedService && selectedService.type === ServiceType.Simple && (
            <SimpleService service={selectedService} startTime={getStartTime()} isAllowSelect={isAllowSelect}
                           duration={selectedDuration} onComplete={addServiceHandler}/>
          )}
          {selectedService && selectedService.type === ServiceType.Photo && (
            <PhotoService service={selectedService} startTime={getStartTime()} isAllowSelect={isAllowSelect}
                          duration={selectedDuration} onComplete={addServiceHandler}/>
          )}
          {selectedService && selectedService.type === ServiceType.HallRent && (
            <HallRentService service={selectedService} startTime={getStartTime()} isAllowSelect={isAllowSelect}
                             duration={selectedDuration} onComplete={addServiceHandler}/>
          )}
          {selectedService && selectedService.type === ServiceType.ItemRent && (
            <ItemRentService service={selectedService} startTime={getStartTime()} isAllowSelect={isAllowSelect}
                             duration={selectedDuration} onComplete={addServiceHandler}/>
          )}
          {selectedService && selectedService.type === ServiceType.Style && (
            <StyleService service={selectedService} startTime={getStartTime()} isAllowSelect={isAllowSelect}
                          duration={selectedDuration} onComplete={addServiceHandler}/>
          )}
        </>
      )}
      <Divider variant={"middle"} orientation={"horizontal"}/>
      <Stack direction={"column"}>
        {addedServices.map((addedService) => (
          <h1>{availableServices[availableServices.findIndex(s => s.id == addedService.service)].title}</h1>
        ))}
        <Divider variant={"middle"} orientation={"horizontal"}/>
        <Button variant={"contained"} disabled={!addedPackage && addedServices.length === 0}
                onClick={handleBuy}>Оформить</Button>
      </Stack>
    </Stack>
  )
}

export type {NewOrderTabProps}
export {NewOrderTab}
