# BackgroundServicePoC
Prova de conceito de BackgroundService + Queues para processamento assíncrono de collections

* A classe Application posta numa fila singleton todos os registros da collection
* Os itens da collection são postados na queue BackgroundTaskQueue
* O serviço de segundo plano (BackgroundService) MainWorker consome os itens dessa fila de forma assíncrona
* Os itens consumidos pelo BackgroundService funcionam como um delegate, o ponto de processamento está definido na função BuildTask, de Application.cs
* É necessário definir a quantia de itens que a fila suportará (em Program.cs), de modo que uma quantia de itens postados que supere esse limite, ficarão esperando vagar lugar na fila para então entrarem
