import {createAction} from "@reduxjs/toolkit";

export const AddedAvatar = createAction<string>("ADDED_AVATAR");
export const AvatarError = createAction<string>("AVATAR_ERROR");
export const UpdateUser = createAction<any>("UPDATE_USER");
