import {useEffect, useRef} from 'react';
import {useSnackbar} from 'notistack';
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "../redux/store";
import {snackbarActions} from "../redux/slices/snackbarSlice";

function NotifyCard() {
    const {enqueueSnackbar} = useSnackbar();
    const viewed = useRef<number[]>([])
    const dispatch = useDispatch();
    const state = useSelector((state: RootState) => state.messages.state);

    useEffect(() => {
        state.forEach((s) => {
            if (viewed.current.indexOf(s.key) === -1) {
                enqueueSnackbar(s.value, {
                    variant: s.variant,
                    key: s.key,
                    onEnter: () => {
                        viewed.current.push(s.key);
                    },
                    onExited: () => {
                        viewed.current = viewed.current.filter(v => v !== s.key);
                        dispatch(snackbarActions.RemoveAction(s.key))
                    },
                })
            }
        })
    }, [state])

    return undefined;
}

export default NotifyCard;