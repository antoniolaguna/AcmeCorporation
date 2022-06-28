## C#

En la primera parte de test de integración he utilizado xunit y testserver para simular el despliegue de la api y probarla.
No he entendido muy bien lo de que queríais la base de datos en real y no en memoria. Quizás habría sido necesario hacer una migración a un sql. Esto se haría cambiando el properties y poniendo la cadena de conexión a la base de datos final. se haria un add-migration y luego un update-database

Por otro lado en cuanto al código y las reglas de negocio, he interpretado que no queríais decoradores en la clase Person de negocio. 
Aquí lo que he hecho, independientemente de que luego me ha venido bien para las restricciones, es crear un DTO de Persona. Creo que no es correcto publicar la clase de negocio con todos sus atributos, sino utilizar un dto (data transfer object) que en realidad en una replica de esta clase de negocio pero unicamente con los atributos que sean necesarios publicar y que vea el "cliente". Me ha venido muy bien uno, para quitar el campo id que se requería, y dos para aplicar decoradores sin tocar la clase Person de negocio, la cual afectaría a la estructura de la base de datos.
No se si es lo correcto la verdad, pero no he entendido muy bien cual era el objetivo de no usar Data Annotations decorators en la clase de negocio. Imagina que es porque no queremos afectar a la estructura de la tabla en la base de datos.

He subido todo el codigo a mi repositorio de github (no me ha dado tiempo a quitar los binarios con git ignore):


´
### Query  1

SELECT year(h.OrderDate) as año,d.ProductID as Producto, cast(ROUND(sum(lineTotal), 2) as float) as suma from sales.salesorderdetail d
inner join sales.SalesOrderHeader h on d.SalesOrderID=h.SalesOrderID
inner join Production.Product p on p.ProductID=d.ProductID
where year(h.OrderDate) in ('2011','2012') and p.ProductNumber like 'FR-%' and p.Color='Black' 
group by year(h.OrderDate),d.ProductID
HAVING cast(ROUND(sum(lineTotal), 2) as float)> 3000000
order by year(h.OrderDate) desc,d.ProductID desc

### Query  2

select year(h1.OrderDate) as mes,month(h1.OrderDate) as anio, cast(ROUND(sum(totalDue), 2) as float) as total, 
(select cast(ROUND(sum(totaldue), 2) as float) from sales.SalesOrderHeader h2 where MONTH(h2.OrderDate)<=MONTH(h1.OrderDate) and year(h2.orderdate)=year(h1.OrderDate)) as sumatorioanio,
(select cast(ROUND(sum(totaldue), 2) as float) from sales.SalesOrderHeader h3 where MONTH(h3.OrderDate)=MONTH(h1.OrderDate)-1 and year(h3.orderdate)=year(h1.OrderDate)) as mesanterior

from sales.SalesOrderHeader h1 
group by year(h1.OrderDate),month(h1.OrderDate)
order by year(h1.OrderDate) asc,month(h1.OrderDate) asc

### Query  3

select distinct(p.BusinessEntityID),p.FirstName,p.LastName,a.AddressLine1 from person.person p
inner join person.BusinessEntityAddress b on b.BusinessEntityID=p.BusinessEntityID
inner join person.Address a on a.AddressID=b.AddressID
order by p.BusinessEntityID

