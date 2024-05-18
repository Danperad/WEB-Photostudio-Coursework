import {Client} from "@models/*";
import {TableCell, TableRow,} from "@mui/material";

type ClientRowProps = {
  client: Client,
  onRowClick: () => void,
}

function ClientRow(props: ClientRowProps) {
  const {client, onRowClick} = props;
  return (
    <TableRow onClick={e => {
      e.preventDefault()
      onRowClick()
    }} sx={{cursor: "pointer"}}>
      <TableCell>{client.lastName}</TableCell>
      <TableCell>{client.firstName}</TableCell>
      {client.middleName ? (
        <TableCell>{client.middleName}</TableCell>
      ) : (
        <TableCell>-</TableCell>
      )}
      <TableCell>{client.phone}</TableCell>
      <TableCell>{client.eMail}</TableCell>
    </TableRow>
  )
}

export type {ClientRowProps};
export {ClientRow};
