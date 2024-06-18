import {useEffect, useRef, useState} from 'react';
import {Box, Button, Card, CardContent, CardMedia, Stack, Typography} from '@mui/material';
import {useDispatch, useSelector} from "react-redux";
import {AppDispatch, RootState} from "../redux/store";
import PackageService from "../services/PackageService";
import ServicePackageModal from "../components/ServicePackageModal";
import {ServicePackage} from "../models/Models";

export default function ServicePackages() {
    const key = useRef<boolean>(false);
    const [openInfoModal, setOpenInfoModal] = useState(false);
    const [selectedService, setSelected] = useState<ServicePackage | null>(null);

    const rootState = useSelector((state: RootState) => state.packages);
    const dispatch = useDispatch<AppDispatch>();

    const closeInfoModal = () => {
        setSelected(null);
        setOpenInfoModal(false);
    };

    const handleInfoModalOpen = (service: ServicePackage) => {
        setSelected(service);
        setOpenInfoModal(true);
    };

    useEffect(() => {
        if (key.current) return;
        key.current = true;
        PackageService.getServices().then((res) => {
            dispatch(res);
        })
    }, [])

    return (
        <div style={{width: "100%"}}>
            <Box sx={{width: "90%", margin: '0 auto', marginTop: '40px'}}>
                <Stack spacing={2}>
                    {rootState.map((pack, index) => (
                        <Card key={index}>
                            <Stack direction={'row'}>
                                <CardMedia
                                    component={"img"}
                                    height={"140"}
                                    sx={{width: '40%'}}
                                    image={pack.photos[0]}
                                    alt="photo"
                                />
                                <CardContent>
                                    <Typography gutterBottom variant="h5" component="div">
                                        {pack.title}
                                    </Typography>
                                    <Typography variant="body2" color="text.secondary">
                                        {pack.description}
                                    </Typography>
                                </CardContent>
                                <Stack direction="row" justifyContent="space-between" alignItems={'flex-end'} mr={2}
                                       ml={2}
                                       pb={1} spacing={1}>
                                    <Typography variant="subtitle1" style={{whiteSpace: "nowrap"}}>
                                        Стоимость: {pack.cost} рублей
                                    </Typography>
                                    <Button size="medium" variant="contained" color="secondary"
                                            onClick={() => {
                                                handleInfoModalOpen(pack)
                                            }}>Подробнее</Button>
                                </Stack>
                            </Stack>
                        </Card>
                    ))}
                </Stack>
            </Box>
            <ServicePackageModal open={openInfoModal} handlerClose={closeInfoModal} service={selectedService}/>
        </div>
    );
}