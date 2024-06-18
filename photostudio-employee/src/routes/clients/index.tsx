import {ClientsTable} from "./ClientsTable.tsx";
import {useEffect, useState} from "react";
import {Button, Divider, Stack, TextField} from "@mui/material";
import {Client} from "@models/*";
import {NewClientTab} from "./NewClientTab.tsx";
import {AboutClient} from "./AboutClient.tsx";
import {getClients} from "../../services/clientService.ts";

enum RightTabStatus {
  Empty,
  Orders,
  NewClient
}

function Clients() {
  const [clients, setClients] = useState<Client[]>([])
  const [isNewClient, setIsNewClient] = useState<boolean>(false)
  const [currentClient, setCurrentClient] = useState<Client | undefined>(undefined)
  const [currentRightTabStatus, setRightTabStatus] = useState(RightTabStatus.Empty)
  const [search, setSearch] = useState<string>(``)

  useEffect(() => {
    const searchClients = setTimeout(() => {
      searchClient()
    }, 500)
    return () => clearTimeout(searchClients)
  }, [search]);

  useEffect(() => {
    if (currentClient) {
      setRightTabStatus(RightTabStatus.Orders)
      return;
    }
    if (isNewClient) {
      setRightTabStatus(RightTabStatus.NewClient)
      return;
    }
    setRightTabStatus(RightTabStatus.Empty)
  }, [currentClient, isNewClient])

  const searchClient = () => {
    getClients(search ? search : undefined).then(res => {
      if (res.ok) {
        setClients(res.val)
      } else {
        console.log(res.val)
      }
    }).catch(error => console.log(error))
  }

  const selectNewClient = () => {
    setCurrentClient(undefined)
    setIsNewClient(true)
  }

  const selectClient = (client: Client) => {
    setCurrentClient(client)
  }

  const closeRightTab = () => {
    setCurrentClient(undefined)
    setIsNewClient(false)
  }

  return (
    <Stack direction={"row"} width={"100%"}>
      <Stack direction={"column"} width={"50%"}>
        <Stack direction={"row"} sx={{pl: 2, mt: 1}} spacing={2}>
          <TextField label={"Поиск"} size={"small"} value={search} onChange={e => {
            setSearch(e.target.value)
          }}></TextField>
          <Button variant={"contained"} onClick={selectNewClient}>
            Новый клиент
          </Button>
        </Stack>
        <ClientsTable clients={clients} onClientSelect={selectClient}/>
      </Stack>
      <Divider orientation={"vertical"} variant={"middle"} flexItem/>
      {currentRightTabStatus === RightTabStatus.NewClient && (
        <NewClientTab close={closeRightTab} onClientCreated={() => {
          closeRightTab()
          searchClient()
        }}/>
      )}
      {currentRightTabStatus === RightTabStatus.Orders && currentClient && (
        <AboutClient client={currentClient} close={closeRightTab}/>
      )}
    </Stack>
  )
}

export {Clients};
