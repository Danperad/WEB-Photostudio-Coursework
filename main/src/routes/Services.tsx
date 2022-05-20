import {Card, Stack, CardHeader, CardContent, Typography, ImageList, ImageListItem} from '@mui/material';
import React from 'react';
import {ServiceModel} from "../models/Models";
import ServicesService from "../services/ServicesService";

let key = false;

function Services() {
	const [services, setServices] = React.useState<ServiceModel[]>([])
	React.useEffect(() => {
		if (key) return;
		ServicesService.getServices().then((res) => {
			setServices(res);
		}).catch((err) => {
				alert("s")
			}
		)
		key = true;
	}, [services])
	return (
		<Stack>
			{services.map((service) => (
				<Card key={service.Title}>
					<CardHeader title={service.Title}/>
					<CardContent>
						<Typography>{service.Description}</Typography>
					</CardContent>
					<Typography variant={"h6"} fontWeight={"normal"}
											fontFamily={"sans-serif"}>Цена: {service.Cost} р.</Typography>
					<ImageList cols={3}>
						{service.Photos.map((photo) => (
							<ImageListItem key={photo}>
								<img src={photo} alt={'#'}/>
							</ImageListItem>
						))}
					</ImageList>
				</Card>
			))}
		</Stack>
	)
}

export default Services;