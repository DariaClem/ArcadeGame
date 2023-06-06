# ArcadeGame

[Română :romania:](#pengutrip-romania)

[English :eu:](#pengutrip-eu)

![prezentare8](https://user-images.githubusercontent.com/91901282/236703372-da5be053-dbe5-4ab5-8c81-aadc0411feab.png)

# PenguTrip :romania:

### Descriere

Aventura Uimitoare a lui Pengu

Porniți într-o călătorie memorabilă cu Pengu în căutarea hărții sale pierdute. Construiți poduri, săriți pe nori și peste prăpăstii periculoase pentru a descoperi misterele acestui univers uimitor.

Dar amintiți-vă, uneori călătoria este mai importantă decât destinația. Sunteți pregătiți să vă alăturați lui Pengu în căutarea sa epică?

Pagina oficială a jocului: https://dariaclem.github.io/Presentation-Site-PenguTrip/

GitHub ul paginii oficiale: https://github.com/DariaClem/Presentation-Site-PenguTrip

Live DEMO: https://www.youtube.com/watch?v=5kQNpJEjYLY

### Informații

Proiectul constă în crearea unui joc arcade. Scopul jocului este de a obține un scor cât mai mare, evitând obstacolele de pe parcurs. Dacă personajul va cădea de pe hartă, scorul va fi resetat.

Pe parcursul jocului, vor apărea obiecte care vor modifica modul de joc atunci când sunt colectate, oferind utilizatorului o experiență a mai multor jocuri consacrate, într-o formă modernă.

### User stories

1. Ca jucător, vreau să pot controla personajul cu ajutorul tastelor A, D și space, astfel încât să pot evita obstacolele și să obțin un scor cât mai mare.
2. Ca jucător, vreau să pot vedea scorul meu curent în timp real, astfel încât să știu cât de bine mă descurc în joc.
3. Ca jucător, vreau să pot colecta obiecte pe parcurs, astfel încât să pot schimba modul de joc.
4. Ca jucător, vreau să pot vedea cel mai bun punctaj al meu, astfel încât să pot încerca să îmi depășesc propriul scor.
5. Ca jucător, vreau să pot obține recompense în joc atunci când îndeplinesc anumite obiective, cum ar fi atingerea unui scor minim sau a unui nivel anume.
6. Ca jucător, vreau să pot personaliza aspectul jocului, cum ar fi background-ul.
7. Ca jucător, vreau ca progresul în joc să fie salvat, astfel încât să pot relua jocul de unde am rămas și să nu pierd scorul și recompensele obținute.
8. Ca jucător, vreau să pot explora diferite moduri de joc, astfel încât să am parte de varietate și să descopăr noi provocări și surprize.
9. Ca jucător, vreau să pot urmări povestea pinguinului în căutarea hărții sale, astfel încât să nu devină monoton jocul.
10. Ca un jucător nou, vreau să am acces la un tutorial informativ înainte de a începe jocul, astfel încât să înțeleg obiectivele și abilitățile disponibile pentru a avea o experiență plăcută.

### Diagramă

În cadrul proiectelor care pun accent pe munca în echipă, o reprezentare grafică pentru a vizualiza elementele jocului și modul în care acestea relaționează sporește productivitatea echipei, făcând fiecare funcționalitate a jocului să fie concisă la implementare. 

Pentru jocul “PenguTrip” a fost realizată următoarea diagramă care ilustrează stările jocului, acțiunile pe care personajul (controlat de utilizator) le poate face, precum și stările în care aceste acțiuni pot fi făcute. 

![workflowDiagram](https://github.com/DariaClem/ArcadeGame/assets/91968875/cec3fdc1-8ded-40d5-a34e-9014300a1ca6)

### Refactoring, code standards

În procesul de scriere a codului ne-am ghidat după standardele de cod stabilite de Google (https://google.github.io/styleguide/csharp-style.html.).

Când am început să lucrăm la proiect nu știam propriu-zis ce presupune dezvoltarea unui joc. Astfel, pe lângă utilizarea IDE-ului UnityEditor, am fost nevoiți să învățăm cum se face legătura dintre ceea ce lucram acolo și codul din C#. Astfel, folosindu-ne de ceea ce ne punea la dispoziție documentația Unity și alte surse specializate, am dobândit experiență în scrierea codului și formarea legăturii cu jocul, ceea ce ne-a ajutat să avem clean-code, ușor de înțeles și ordonat. Dacă la început am pus în același loc tot ce am găsit, odată cu avansarea în procesul de creare am început să structurăm codul în funcții dedicate.

O parte din standardele implementate sunt:

1. Datele membre sunt grupate după modificatorul de acces în ordinea public, private.
2. Variabilele private sunt de forma _nume.
3. Variabilele sunt de tipul camelCase.
4. Denumirile funcțiilor și a clasei sunt de tipul PascalCase.
5. Este menționat tipul de date al fiecărei variabile, în loc de a folosi „var”.
6. Anumite variabile sunt inițializate de la declarare (acolo unde se poate).

### Design Patterns

Utilizare Singleton:
Am utilizat acest design pattern pentru a avea o singură instanță a anumitor obiecte și pentru a le putea folosi în mai multe scene ale jocului (în special sound-urile). 

Utilizarea Singleton-ului a ajutat la rezolvarea problemei în care obiecte erau multiplicate de fiecare dată când era accesată scena în care erau generate. 

### AI Tools

În procesul de realizare al jocului am avut câteva momente în care ne-am împotmolit. Cum nu am reușit să găsim o soluție concretă pe internet am folosit ChatGPT. 

Cu toate acestea a fost o sarcină destul de dificilă de a obține un răspuns care să ne ajute în problema pe care o întâmpinasem. Programul oferea adesea răspunsuri greșite (prezenta fie soluții inexistente, i.e. biblioteci inexistente, fie soluții care nu funcționau pentru cazul dat). Am reușit totuși, prin indicațiile date de tool, să înțelegem conceptele pe care trebuia să le folosim și cum să le aplicăm pe nevoile noastre. Se poate spune astfel că am învățat din greșelile acestuia. 



# PenguTrip :eu:

### Description

The Ultimate Pengu Adventure

Embark on a journey of a lifetime with Pengu as he searches for his lost map. Build bridges, jump on clouds and over dangerous gaps to discover the mysteries of this amazing world.

But remember, sometimes the journey is more important than the destination. Are you ready to join Pengu on his epic quest?

### Information
The project consists of creating an arcade game. The goal of the game is to achieve the highest score possible by avoiding obstacles along the way. If the character falls off the map, the score will be reset.

Throughout the game, there will be objects that will modify the gameplay when collected, providing the user with an experience of multiple popular games in a modern form.

### User stories
1. As a player, I want to be able to control the character using the A, D, and space keys so that I can avoid obstacles and achieve a high score.
2. As a player, I want to see my current score in real-time so that I know how well I'm doing in the game.
3. As a player, I want to be able to collect objects along the way to change the gameplay.
4. As a player, I want to see my highscore so that I can try to surpass my own score.
5. As a player, I want to receive rewards in the game when I achieve certain objectives, such as reaching a minimum score or a specific level.
6. As a player, I want to be able to customize the game's appearance, such as the background.
7. As a player, I want the progress in the game to be saved so that I can resume the game where I left off and not lose my score and rewards.
8. As a player, I want to explore different game modes so that I can have variety and discover new challenges and surprises.
9. As a player, I want to follow the story of the penguin in search of its map so that the game doesn't become monotonous.
10. As a new player, I want to have access to an informative tutorial before starting the game so that I understand the objectives and available abilities to have an enjoyable experience.

### Diagram

Within teamwork-oriented projects, a graphical representation to visualize the game elements and how they relate enhances team productivity, making each game functionality concise to implement.

For the game "PenguTrip," the following diagram has been created, illustrating the game states, the actions that the user-controlled character can perform, and the states in which these actions can be done.

![workflowDiagram](https://github.com/DariaClem/ArcadeGame/assets/91968875/e089f262-7f07-40bf-b53f-39da90010995)

### Refactoring, code standards

During the process of writing the code, we followed the coding standards established by Google (https://google.github.io/styleguide/csharp-style.html).

When we started working on the project, we didn't really know what game development entailed. Therefore, in addition to using the UnityEditor IDE, we had to learn how to link what we were working on to the C# code. By leveraging the resources provided by the Unity documentation and other specialized sources, we gained experience in writing code and establishing the connection with the game. This helped us achieve clean code that is easy to understand and well-organized. Initially, we put everything we found in the same place, but as we progressed in the development process, we began to structure the code into dedicated functions.

Some of the implemented standards include:

1. Member data is grouped based on the access modifier in the order of public, private.
2. Private variables are in the form of "_name".
3. Variables are in camelCase.
4. Function and class names are in PascalCase.
5. The data type of each variable is specified instead of using "var".
6. Certain variables are initialized upon declaration (where possible).

### Design patterns

Singleton Usage:
We utilized this design pattern to have a single instance of certain objects and to be able to use them in multiple scenes of the game (especially for sounds).

The usage of Singleton helped solve the problem where objects were duplicated every time the scene in which they were generated was accessed.

### AI Tools

During the game development process, we encountered several moments where we got stuck. As we couldn't find a concrete solution on the internet, we turned to ChatGPT.

However, obtaining a helpful response to our problem was quite challenging. The program often provided incorrect answers (either suggesting non-existent solutions, such as non-existing libraries, or proposing solutions that didn't work for our specific case). Nevertheless, by following the indications provided by the tool, we managed to understand the concepts we needed to apply and how to tailor them to our needs. It can be said that we learned from its mistakes.
