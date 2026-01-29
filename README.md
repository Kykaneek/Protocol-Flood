**Dokumentacja projektu:** [Bezpieczeństwo_Sieciowe_KG.pdf](https://github.com/user-attachments/files/24947592/Bezpieczenstwo_Sieciowe_KG.pdf)


<h1>Protocol Flood</h1>

Ninniejsza aplikacja została stworzona na potrzeby przedmiotu **Bezpieczeństwo sieciowe** 

Składa się z 3 części: 

 - Aplikacji UI Windows Forms - instalujemy na stacji roboczej - branch master
 - Aplikacji TCPServer - instalujemy na serwerze - branch backend
 - Flooder - instalujemy na serwerze - branch flooder

Każda z częśći aplikacji znajduje się na innym branchu, iż każda znich to jest osobny projekt <br>
Wszystkie aplikacje uruchamiamy w Visual Studio code W celu ich pierwornemu przetestowania

***UWAGA!*** 

1. Aplikację z flooderem uruchamiamy ***TYLKO*** w środowisku hermetycznym - jest to projekt edukacyjny
2. Należy przygorować środowisko wirtualne składające się z 3 maszyn - 1 maszyny Windows 10/11 oraz 2 Maszyn z Windows Server 2016
3. Aplikacje TCPServer i Flooder instalujemy na maszynach Wirtualnych tylko z Windowsem Server z zainstalowanym **Docker Desktop** oraz **WSL2** z zainstalowaną dystrybucją linux - w moim wypadku było to ubuntu 

<h3>ClientTCP</h3>
Uwaga! - Wszystkie czynności możemy wykonać na maszynie wirtualnej z Windowsem 10 / Windowsem 11 bądź na standardowej maszynie - aplikację trzeba wykompilować,<br>
następnie można ją przenieść na środowisko maszyny wirtualnej - według uznania

1. Aby pobrać klientaTCP należy włączyć IDE Visual Studio Community i pobrać repozytorium → wybierz ***Klonuj Repozytorium***<br>
<img width="770" height="533" alt="image" src="https://github.com/user-attachments/assets/f16a3bd5-9a66-4869-afee-84b2b8ce73fb" />

2. Następnie należy wprowadzić loklaizacje repozytorium zdalnego wprowadzając url z <br>
[Git](https://github.com/Kykaneek/Protocol-Flood/) → Code  → HTTPS → https://github.com/Kykaneek/Protocol-Flood.git<br>
Następnie wybierz **Klonuj**<br>
<img width="770" height="533" alt="image" src="https://github.com/user-attachments/assets/2a549085-9dc5-4c27-8b1c-837000c65aa9" />

3. Po pobraniu repozytorium wybierz **Eksplorator rozwiązań** a następnie kliknij prawym przyciskiem myszy na ***Rozwiązanie "Client"*** i wybierz **Kompiluj rozwiązanie**<br>
<img width="276" height="366" alt="image" src="https://github.com/user-attachments/assets/75e9e762-1a7e-4f21-a314-9a7109406610" /><br>

4. Po skompilowaniu ponownie wybierz **Eksploraor rozwiąza** → ***Rozwiązanie "Client"*** → ***Otwórz w Eksploratorze plików***
5. **Przejdź ścieżkę**<br>
***Client*** → bin → Debug → net10.0-windows → ***Client.exe***<br>
<img width="425" height="774" alt="image" src="https://github.com/user-attachments/assets/5d35b4e9-a38a-4f58-84ab-e320cb93b1c9" /><br>
Aplikacja została uruchomiona poprawnie - można spakować folder **net10.0-windows** i wypakować aplikację na maszynie wirtualnej z Windowsem 10 / 11


<h3>SerrverTCP i Flooder</h3>

Przebieg instalacyjny jest ten sam dla jednej i drugiej aplikacji. Należy przejść poniższe kroki

<h4>Visual Studio</h4>

1. Wykonaj kroki 1 i 2 z rozdziału **ClientTCP** w miejscu, gdzie chcesz zainstalować aplikacje<br>
2. W Visual Studio Community wybierz w prawym dolnym rogu zakładkę **master** → Elementy zdalne → *origin/flooder | origin/backend* w zależności od miejsca instalacji<br>
<img width="372" height="315" alt="image" src="https://github.com/user-attachments/assets/09bc9702-c179-4d10-804e-f9f0e0c565ca" /><br>
3. Po pobraniu repozytorium wybierz **Eksplorator rozwiązań** a następnie kliknij prawym przyciskiem myszy na ***Rozwiązanie "Client"*** i wybierz **Kompiluj rozwiązanie**<br>
4. Po pobraniu repozytorium wybierz **Eksplorator rozwiązań** a następnie kliknij prawym przyciskiem myszy na ***Opublikuj***<br>
<img width="403" height="214" alt="image" src="https://github.com/user-attachments/assets/a41e7c3c-6924-4db2-b1c8-e89d9a613368" /><br>
5. Wybierz ***Dodaj pprofil publikowania*** → Cel **Folder** przycik  ***Dalej*** → Konkretny element docelowy **Folder** przycik  ***Dalej*** → Lokalizacja **Lokalizacja folderu** przycik  ***Przeglądaj*** - wybierz folder w którym chcesz by aplikacja się znajdowała → Następnie przycik ***Koniec** → **Zamknij**<br>
<img width="1401" height="629" alt="image" src="https://github.com/user-attachments/assets/cede93de-397a-4670-9bfe-1abf01873494" /><br>
6. Wybierz **Publikuj**<br>
<img width="888" height="377" alt="image" src="https://github.com/user-attachments/assets/88cace18-e7e1-4581-97df-acc570ce123d" />

<h4>CMD/PowerShell</h4>

1. Włącz konsoleę PowerShell lub CMD i przejdź do folderu z aplikacją[Dockerfile.txt](https://github.com/user-attachments/files/24947415/Dockerfile.txt)

2. Stwórz podstawowy plik dockerfile → ***Przykład*** [Uploading Dockerfile…]()

3. Wykonaj polecenie `docker build -t `*`nazwa aplikacji`*` .` np. `docker build -t tcpserver .`
4.  Wykonaj polecenie `docker run -p 5000:5000  `*`nazwa aplikacji`*` .` np. `docker run -p 5000:5000 tcpserver .`

**Uwaga** → q przypadku aplikacji Flooder należy wybrać inny port niż 5000 np. 4000

