import {Button, IconButton, Stack} from "@mui/material";
import {FormContainer, TextFieldElement} from 'react-hook-form-mui'
import {Client} from "@models/*";
import {Close} from "@mui/icons-material";
import {addClient} from "../../services/clientService.ts";

const clearClient = (): Client => {
  return {
    id: undefined,
    lastName: ``,
    firstName: ``,
    middleName: ``,
    phone: ``,
    eMail: ``
  }
}

type NewClientTabProps = {
  onClientCreated: () => void
  close: () => void
}

function NewClientTab(props: NewClientTabProps) {
  const {onClientCreated ,close} = props
  const handleAddClient = (c: Client) => {
    addClient(c).then(res => {
      if (res.ok) {
        onClientCreated()
      }
      else{
        console.log(res.val);
      }
    }).catch(err => console.log(err))
  }

  return (
    <Stack direction={"column"} width={"50%"}>
      <IconButton onClick={close}>
        <Close/>
      </IconButton>
      <FormContainer defaultValues={clearClient()} onSuccess={handleAddClient}>
        <Stack direction={"column"}>
          <TextFieldElement label={"Фамилия"} name={"lastName"} size={'small'} required/>
          <TextFieldElement label={"Имя"} name={"firstName"} size={'small'} required/>
          <TextFieldElement label={"Отчество"} name={"middleName"} size={'small'}/>
          <TextFieldElement label={"Телефон"} type={"tel"} name={"phone"} size={'small'} required/>
          <TextFieldElement label={"Почта"} type={"email"} name={"eMail"} size={'small'} required/>
          <Button type={'submit'} variant={"contained"}>Добавить</Button>
        </Stack>
      </FormContainer>
    </Stack>
  )
}

export type {NewClientTabProps}
export {NewClientTab};
