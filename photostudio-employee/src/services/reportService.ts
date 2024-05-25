import {Err, Ok, Result} from "ts-results";
import {AxiosError} from "axios";
import axiosInstant from "../utils/axiosInstant.ts";

export async function getReport(start: Date, end: Date) : Promise<Result<boolean, AxiosError>> {
  return axiosInstant.get('reports', {
    params: {
      startDate: start,
      endDate: end
    },
    responseType: 'blob'
  }).then(res => {
    const href = URL.createObjectURL(new Blob([res.data]));

    // create "a" HTML element with href to file & click
    const link = document.createElement('a');
    link.href = href;
    link.setAttribute('download', 'report.xlsx'); //or any other extension
    document.body.appendChild(link);
    link.click();

    // clean up "a" element & remove ObjectURL
    document.body.removeChild(link);
    URL.revokeObjectURL(href);
    return Ok(true)
  }).catch(error => {
    const err = error as AxiosError;
    return Err(err);
  })
}
