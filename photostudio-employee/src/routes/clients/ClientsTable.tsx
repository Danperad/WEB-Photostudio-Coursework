import {Client} from "@models/*";
import {ClientRow} from "./ClientRow.tsx";
import {Table, TableBody, TableCell, TableContainer, TableHead, TableRow} from "@mui/material";

type ClientsTableProps = {
  clients: Client[],
  onClientSelect: (client: Client) => void
}

function ClientsTable(props: ClientsTableProps) {
  const {clients, onClientSelect} = props
  return (
    <TableContainer>
      <Table stickyHeader style={{width: '100%'}}>
        <TableHead>
          <TableRow sx={{cursor: "default"}}>
            <TableCell>Фамилия</TableCell>
            <TableCell>Имя</TableCell>
            <TableCell>Отчество</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {
            clients.map((client: Client) => (
              <ClientRow key={client.id} client={client} onRowClick={() => {
                onClientSelect(client)
              }}/>
            ))
          }
        </TableBody>
      </Table>
    </TableContainer>
  )
}

export type {ClientsTableProps};
export {ClientsTable};
