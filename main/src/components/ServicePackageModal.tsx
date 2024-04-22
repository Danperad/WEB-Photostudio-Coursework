import {useState} from 'react';
import {Box, Button, Modal, Stack, Typography} from "@mui/material";
import {ServicePackage} from "../models/Models";
import AddServicePackageModal from "./AddServicePackageModal";
import Carousel from 'react-material-ui-carousel'

interface ServicePackageModalProps {
    open: boolean,
    handlerClose: () => void,
    service: ServicePackage | null
}

export default function ServicePackageModal(props: ServicePackageModalProps) {
    const handlePayModalOpen = () => {
        setOpenPayModal(true);
    };
    const closePayModal = () => {
        setOpenPayModal(false);
    };
    const [openPayModal, setOpenPayModal] = useState(false);

    const style = {
        position: 'absolute' as const,
        top: '50%',
        left: '50%',
        transform: 'translate(-50%, -50%)',
        width: '60%',
        bgcolor: '#F0EDE8',
        border: '2px solid #776D61',
        borderRadius: '20px',
        boxShadow: 24,
        p: 4,
    };

    if (props.service === null) return <></>;
    return (
        <Modal
            open={props.open}
            onClose={props.handlerClose}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
        >
            <Box sx={style}>
                <Stack spacing={2} width={"100%"} mt={2} alignItems="center" justifyContent={"center"}>
                    <Stack alignItems="center" justifyContent={"center"}
                           style={{height: "400px", width: "600px", borderRadius: "10px"}}>
                        <Carousel animation={"fade"} indicators={false} sx={{
                            width: "100%",
                            height: "100%"
                        }}>
                            {props.service!.photos.map((photo, index) => (
                                <img
                                    src={photo}
                                    alt={props.service!.title}
                                    key={index}
                                    width="100%"
                                    height="100%"
                                />
                            ))}
                        </Carousel>
                    </Stack>
                    <Box sx={{width: "80%"}}>
                        <Typography variant="subtitle1">
                            Название: {props.service!.title}
                        </Typography>
                        <Typography variant="subtitle1">
                            Описание: {props.service!.description}
                        </Typography>
                    </Box>
                    <Stack direction="row" width={"80%"} justifyContent="space-between" alignItems="center">
                        <div style={{width: "100px"}}></div>
                        <Stack direction="row" spacing={2}>
                            <Typography variant="subtitle1">
                                Стоимость: {props.service!.cost} рублей
                            </Typography>
                            <Button variant="contained" color="secondary" size="medium"
                                    disableElevation
                                    sx={{borderRadius: '10px'}} onClick={() => handlePayModalOpen()}>
                                Добавить в корзину
                            </Button>
                        </Stack>
                    </Stack>
                </Stack>
                <AddServicePackageModal open={openPayModal} handlerClose={closePayModal} service={props.service}/>
            </Box>
        </Modal>
    );
}