import {
  Button,
  Divider,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography
} from "@mui/material";
import {Service} from "@models/*";
import {useEffect, useRef, useState} from "react";
import {getServices} from "../../services/serviceService.ts";
import {ServiceType} from "../../models/Service.ts";
import {FormContainer, TextFieldElement} from "react-hook-form-mui";

function Services() {
  const [services, setServices] = useState<Service[]>([])
  const [selectedService, setSelectedService] = useState<Service | undefined>(undefined)
  const key = useRef(false);

  useEffect(() => {
    if (key.current)
      return
    key.current = true
    getServices().then(res => {
      if (res.ok) {
        setServices(res.val)
      } else {
        console.log(res.val)
      }
    })
  }, []);

  const serviceTypeToString = (type: ServiceType) => {
    switch (type) {
      case ServiceType.Style:
        return "Стилист"
      case ServiceType.Photo:
        return "Фото"
      case ServiceType.ItemRent:
        return "Аренда вещей"
      case ServiceType.Simple:
        return "Простая"
      case ServiceType.HallRent:
        return "Аренда зала"
    }
  }

  return (
    <Stack direction={"row"} width={"100%"}>
      <Stack width={"50%"}>
        <Stack direction={"row"} sx={{pl: 2, mt: 1}} spacing={2}>
          <Button variant={"contained"} onClick={() => {
            setSelectedService(undefined)
          }}>
            Новая услуга
          </Button>
        </Stack>
        <TableContainer>
          <Table stickyHeader style={{width: '100%'}}>
            <TableHead>
              <TableRow sx={{cursor: "default"}}>
                <TableCell>Название</TableCell>
                <TableCell>Описание</TableCell>
                <TableCell>Цена</TableCell>
                <TableCell>Тип</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {services.map(s => (
                <TableRow key={s.id} sx={{cursor: "pointer"}} onClick={() => {
                  setSelectedService(s)
                }}>
                  <TableCell>{s.title}</TableCell>
                  <TableCell>{s.description}</TableCell>
                  <TableCell>{s.cost}</TableCell>
                  <TableCell>{serviceTypeToString(s.type)}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Stack>

      <Divider orientation={"vertical"} variant={"middle"} flexItem/>
      <Stack width={"50%"} spacing={1} p={1}>
        <Typography
          variant={"h6"}>{selectedService ? `Редактирование услуги: ${selectedService.title}` : `Новая услуга`}</Typography>
        <FormContainer>
          <Stack direction={"column"} spacing={1} sx={{px: 2}}>
            <TextFieldElement label={"Название"} name={"title"} size={'small'} required/>
            <TextFieldElement label={"Описание"} name={"description"} required/>
            <TextFieldElement label={"Цена"} name={"cost"} size={'small'} required/>
          </Stack>
        </FormContainer>
      </Stack>
    </Stack>
  )
}

export {Services};
