import {OrderServicePackage, ServicePackage} from "@models/*";
import {Button, Stack, Typography} from "@mui/material";

type NewOrderServicePackageProps = {
  servicePackage: ServicePackage,
  isAllowSelect: () => boolean,
  startTime: Date,
  onComplete: (servicePackage: OrderServicePackage) => void
}


function NewOrderServicePackage(props: NewOrderServicePackageProps) {
  const {servicePackage, isAllowSelect, startTime, onComplete} = props

  const handleComplete = () => {
    const orderServicePackage: OrderServicePackage = {
      servicePackage,
      startDateTime: startTime
    }
    onComplete(orderServicePackage)
  }

  return (
    <Stack>
      <Typography variant={"body2"}>{servicePackage.description}</Typography>
      <Button variant={"contained"} onClick={handleComplete} disabled={!isAllowSelect()}>Добавить</Button>
    </Stack>
  )
}

export type {NewOrderServicePackageProps}
export {NewOrderServicePackage}
