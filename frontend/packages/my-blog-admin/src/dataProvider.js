import { stringify } from "query-string";
import { fetchUtils } from "ra-core";

export default (apiUrl, httpClient = fetchUtils.fetchJson) => ({
  getList: (resource, params) => {
    const { page, perPage } = params.pagination;
    const { field, order } = params.sort;

    const query = {
      // sort: JSON.stringify([field, order]),
      // range: JSON.stringify([(page - 1) * perPage, page * perPage - 1]),
      // filter: JSON.stringify(params.filter),
      page: page - 1,
      size: perPage
    };

    const url = `${apiUrl}/${resource}/getList?${stringify(
      query
    )}`;
    return httpClient(url).then(({ headers, json }) => {
      const { items, totalCount } = json;
      return {
        data: items,
        total: totalCount,
      };
    });
  },
  getOne: (resource, params) => {
    const url = `${apiUrl}/${resource}/getOne?id=${params.id}`;
    return httpClient(url).then(({ json }) => {
      console.log(json);
      return ({
        data: json,
      })
    });
  },
  getMany: (resource, params) => {
    const query = {
      ids: params.ids,
    };
    const url = `${apiUrl}/${resource}/getMany?${stringify(query)}`;
    return httpClient(url).then(({ json }) => ({ data: json }));
  },
  getManyReference: (resource, params) => {
    const { page, perPage } = params.pagination;
    const { field, order } = params.sort;
    const query = {
      sort: JSON.stringify([field, order]),
      range: JSON.stringify([(page - 1) * perPage, page * perPage - 1]),
      filter: JSON.stringify(
        Object.assign(Object.assign({}, params.filter), {
          [params.target]: params.id,
        })
      ),
    };
    const url = `${apiUrl}/${resource}/getManyReference?${stringify(query)}`;
    return httpClient(url).then(({ headers, json }) => {
      if (!headers.has("content-range")) {
        throw new Error(
          "The Content-Range header is missing in the HTTP Response. The simple REST data provider expects responses for lists of resources to contain this header with the total number of results to build the pagination. If you are using CORS, did you declare Content-Range in the Access-Control-Expose-Headers header?"
        );
      }
      return {
        data: json,
        total: parseInt(headers.get("content-range").split("/").pop(), 10),
      };
    });
  },
  update: (resource, params) =>
    httpClient(`${apiUrl}/${resource}/update/${params.id}`, {
      method: "PUT",
      body: JSON.stringify(params.data),
    }).then(({ json }) => ({ data: json })),
  // simple-rest doesn't handle provide an updateMany route, so we fallback to calling update n times instead
  updateMany: (resource, params) =>
    Promise.all(
      params.ids.map((id) =>
        httpClient(`${apiUrl}/${resource}/${id}`, {
          method: "PUT",
          body: JSON.stringify(params.data),
        })
      )
    ).then((responses) => ({ data: responses.map(({ json }) => json.id) })),
  create: (resource, params) =>
    httpClient(`${apiUrl}/${resource}/create`, {
      method: "POST",
      body: JSON.stringify(params.data),
    }).then(({ json }) => ({
      data: Object.assign(Object.assign({}, params.data), { id: json.id }),
    })),
  delete: (resource, params) =>
    httpClient(`${apiUrl}/${resource}/${params.id}`, {
      method: "DELETE",
    }).then(({ json }) => ({ data: json })),
  // simple-rest doesn't handle filters on DELETE route, so we fallback to calling DELETE n times instead
  deleteMany: (resource, params) =>
    Promise.all(
      params.ids.map((id) =>
        httpClient(`${apiUrl}/${resource}/${id}`, {
          method: "DELETE",
        })
      )
    ).then((responses) => ({ data: responses.map(({ json }) => json.id) })),
});
