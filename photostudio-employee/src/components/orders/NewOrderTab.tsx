import {ChangeEvent, useEffect, useState} from "react";
import {
  Client,
  Employee,
  Hall,
  Item,
  Order,
  OrderService,
  OrderServicePackage,
  Service,
  ServicePackage
} from "@models/*";
import {ArrowBack, Close} from '@mui/icons-material';
import {
  Button,
  Card,
  CardContent,
  CardHeader,
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
import {getServicesPackage} from "../../services/servicePackageService.ts";

const endDate = dayjs().add(6, 'month')
const startTime = dayjs().set('hour', 8).startOf('hour');
const endTime = dayjs().set('hour', 22).startOf('hour');

type NewOrderTabProps = {
  client: Client,
  onOrderCreated: () => void,
  close: () => void
}

function NewOrderTab(props: NewOrderTabProps) {
  const {client, onOrderCreated, close} = props
  const [availableServices, setAvailableServices] = useState<Service[]>([])
  const [availableServicesPackages, setAvailableServicesPackages] = useState<ServicePackage[]>([])

  const [selectedService, setSelectedService] = useState<Service | undefined>(undefined)
  const [selectedPackage, setSelectedPackage] = useState<ServicePackage | undefined>(undefined)
  const [addedServices, setAddedServices] = useState<OrderService[]>([])
  const [addedPackage, setAddedPackage] = useState<OrderServicePackage | undefined>(undefined)

  const [selectedDate, setSelectedDate] = useState<Dayjs>(dayjs());
  const [selectedTime, setSelectedTime] = useState<Dayjs>(dayjs().set("hour", 8).set("minute", 0));
  const [selectedDuration, setSelectedDuration] = useState<number>(60);

  useEffect(() => {
    getServices().then(res => {
      if (res.ok) {
        setAvailableServices(res.val)
      } else {
        console.log(res.val)
      }
    }).catch(err => console.log(err))
    getServicesPackage().then(res => {
      if (res.ok) {
        setAvailableServicesPackages(res.val)
      } else {
        console.log(res.val)
      }
    })
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

  const getEmployeeFullName = (employee: Employee) => {
    let name = employee.lastName + ` ` + employee.firstName.charAt(0) + `.`
    if (employee.middleName)
      name += ` ` + employee.middleName.charAt(0) + `.`
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
    setSelectedPackage(availableServicesPackages[availableServicesPackages.findIndex((val) => val.id === +event.target.value)])
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

  const fixOrderService = (orderService: OrderService): OrderService => {
    return {
      ...orderService,
      service: (orderService.service as Service).id,
      item: orderService.item ? (orderService.item as Item).id : undefined,
      hall: orderService.hall ? (orderService.hall as Hall).id : undefined,
      employee: orderService.employee ? (orderService.employee as Employee).id : undefined,
    }
  }

  const handleBuy = () => {
    const services: OrderService[] = []
    addedServices.map(serv => services.push(fixOrderService(serv)))
    const order: Order = {
      client: client.id!,
      servicePackage: addedPackage ? {
        ...addedPackage,
        servicePackage: (addedPackage?.servicePackage as ServicePackage).id
      } : undefined,
      services: services
    }
    addNewOrder(order).then(res => {
      if (res.ok) {
        onOrderCreated()
      } else {
        console.log(res.val)
      }
    })
  }

  const getServicePrice = (service: OrderService): number => {
    let price = (service.service as Service).cost;
    switch ((service.service as Service).type) {
      case ServiceType.Simple:
        break
      case ServiceType.HallRent:
        price += (service.hall as Hall).pricePerHour * (service.duration! / 60)
        break
      case ServiceType.ItemRent:
        price += (service.item as Item).costPerHour * service.count! * (service.duration! / 60)
        break
      case ServiceType.Photo:
        price += (service.employee as Employee).cost * (service.duration! / 60)
        break
      case ServiceType.Style:
        price += (service.employee as Employee).cost * (service.duration! / 60) * (service.isFullTime ? 1.5 : 1)

    }

    return price
  }

  const getTotalPrice = (): number => {
    let price = 0;
    if (addedPackage) {
      price += (addedPackage.servicePackage as ServicePackage).cost
    }
    addedServices.forEach(service => price += getServicePrice(service))
    return price
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
                    onChange={dateSelectHandler} sx={{width: "8vw"}} minDate={dayjs()} maxDate={endDate}/>
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
            {availableServicesPackages.map((servicePackage) => (
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
        {addedPackage && (
          <Card>
            <CardHeader title={(addedPackage.servicePackage as ServicePackage).title} action={(
              <IconButton onClick={() => setAddedPackage(undefined)}>
                <Close/>
              </IconButton>
            )}/>
            <CardContent>
              <Typography>Начало: {dayjs(addedPackage.startDateTime).format("DD-MM-YY HH:mm")}</Typography>
              <Typography>Длительность: {(addedPackage.servicePackage as ServicePackage).duration} мин.</Typography>
              <Typography>Стоимость: {(addedPackage.servicePackage as ServicePackage).cost} Р.</Typography>
            </CardContent>
          </Card>
        )}
        {addedServices.map((addedService, i) => (
          <Card key={i}>
            <CardHeader title={(addedService.service as Service).title} action={(
              <IconButton onClick={() => setAddedServices(prevState => prevState.filter(o => o !== addedService))}>
                <Close/>
              </IconButton>
            )}/>
            <CardContent>
              {addedService.employee && (
                <Typography>Сотрудник: {getEmployeeFullName(addedService.employee as Employee)}</Typography>
              )}
              {addedService.hall && (
                <Typography>Зал: {(addedService.hall as Hall).title}</Typography>
              )}
              {addedService.item && (
                <Typography>Предмет: {(addedService.item as Item).title}</Typography>
              )}
              {addedService.count && (
                <Typography>Количество: {addedService.count}</Typography>
              )}
              {addedService.startDateTime && (
                <Typography>Начало: {dayjs(addedService.startDateTime).format("DD-MM-YY HH:mm")}</Typography>
              )}
              {addedService.duration && (
                <Typography>Длительность: {addedService.duration} мин.</Typography>
              )}
              {addedService.isFullTime && (
                <Typography>На всё время</Typography>
              )}
              <Typography>Стоимость: {getServicePrice(addedService)} Р.</Typography>
            </CardContent>
          </Card>
        ))}
        <Divider variant={"middle"} orientation={"horizontal"}/>
        <Typography>Итоговая стоимость: {getTotalPrice()} Р.</Typography>
        <Button variant={"contained"} disabled={!addedPackage && addedServices.length === 0}
                onClick={handleBuy}>Оформить</Button>
      </Stack>
    </Stack>
  )
}

export type {NewOrderTabProps}
export {NewOrderTab}
