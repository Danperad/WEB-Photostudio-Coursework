import {Service} from "../models/Models.ts";
import {Button, Card, CardContent, CardMedia, Stack, Typography} from "@mui/material";

export interface ServiceCartProps {
  service: Service;
  handleInfoModalOpen: () => void
}

export function ServiceCart(props: ServiceCartProps) {
  return (
    <Card key={props.service.id} sx={{mt: 2, bgcolor: '#FFE2E2'}}>
      <Stack direction={'row'}>
        <CardMedia
          component={"img"}
          height={"140"}
          sx={{width: '60%'}}
          image={props.service.photos[0]}
          alt="photo"
        />
        <CardContent sx={{width: "100%"}}>
          <Typography gutterBottom variant="h5" component="div">
            {props.service.title}
          </Typography>
          <Typography variant="body2" color="text.secondary">
            {props.service.description}
          </Typography>
        </CardContent>
        <Stack direction="row" justifyContent={'right'} alignItems={'end'} mr={2}
               ml={2}
               pb={1} spacing={1}>
          <Typography variant="subtitle1" style={{whiteSpace: "nowrap"}}>
            Стоимость: {props.service.cost} рублей
          </Typography>
          <Button size="medium" variant="contained" color="secondary"
                  onClick={() => {
                    props.handleInfoModalOpen()
                  }}>Подробнее</Button>
        </Stack>
      </Stack>
    </Card>
  )
}
