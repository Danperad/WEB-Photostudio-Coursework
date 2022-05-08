import {Card, Stack, CardHeader, CardContent, Typography, ImageList, ImageListItem} from '@mui/material';
import React from 'react';
import {ServiceModel} from "../models/ServiceModel";
import ServicesService from "../redux/services/ServicesService";

function Services(){
	const [services, setServices] = React.useState<ServiceModel[]>([])
	React.useEffect(() => {
		if (services.length !== 0) return;
		ServicesService.getServices().then((res) => {
			setServices(res);
		})
	}, [services])
	return (
		<Stack>
			{services.map((service) => (
				<Card key={service.Title}>
					<CardHeader title={service.Title} />
					<CardContent>
						<Typography>{service.Description}</Typography>
					</CardContent>
					<Typography variant={"h6"} fontWeight={"normal"} fontFamily={"sans-serif"}>Цена: {service.Cost} р.</Typography>
					<ImageList cols={3}>
						{service.Photos.map((photo) => (
							<ImageListItem key={photo}>
								<img src={photo}/>
							</ImageListItem>
						))}
					</ImageList>
				</Card>
			))}
		</Stack>
	)
}

export default Services;