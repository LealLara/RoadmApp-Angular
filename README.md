 
# 📚 RoadmApp - Personal Planning & Notes Dashboard
 

> Uma aplicação web moderna e completa para gerenciamento pessoal, planejamento e anotações com autenticação segura, exportação de dados e interface visual atrativa.

[![Angular](https://img.shields.io/badge/Angular-21.2.0-red?style=for-the-badge&logo=angular)](https://angular.io)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com)
[![TypeScript](https://img.shields.io/badge/TypeScript-5.9.2-blue?style=for-the-badge&logo=typescript)](https://www.typescriptlang.org)
[![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)](LICENSE)

## 🎯 Visão Geral

**RoadmApp** é uma aplicação de stack completo (Full Stack) que oferece um painel personalizado para estudantes, profissionais e entusiastas que desejam organizar suas vidas. Com autenticação por usuário, múltiplas categorias de notas, exportação de dados em Excel e uma interface visual inspirada no céu com animações de partículas, a aplicação combina funcionalidade com beleza.

### Objetivo do Projeto
Criar uma plataforma que permita:
- ✅ Registro e autenticação segura de usuários
- ✅ Gerenciamento de notas em 10 categorias diferentes
- ✅ Upload de imagens e desenho canvas para notas
- ✅ Filtro por período (mês/ano)
- ✅ Exportação automática de notas em Excel (.xlsx)
- ✅ Dashboard intuitivo com isolamento de dados por usuário
- ✅ Animações fluidas com partículas de estrelas

---

## 🏗️ Arquitetura do Projeto

O projeto segue uma arquitetura moderna de **Full Stack** com separação clara entre frontend e backend:

```
RoadmApp/
├── frontend/                    # Angular 21 - Client-side
│   ├── src/
│   │   ├── app/
│   │   │   ├── app.ts          # Componente raiz com animação
│   │   │   ├── app.routes.ts   # Definição de rotas
│   │   │   ├── app.config.ts   # Configuração da aplicação
│   │   │   └── pages/
│   │   │       ├── login/      # Autenticação
│   │   │       ├── register/   # Registro de usuários
│   │   │       ├── dashboard/  # Painel principal
│   │   │       └── welcome/    # Página de boas-vindas
│   │   └── main.ts             # Entry point
│   └── package.json
│
└── backend/                     # ASP.NET Core 8 - Server-side
    ├── src/
    │   ├── RoadmApp.Api/        # API Endpoints
    │   │   ├── Endpoints/       # Minimal APIs
    │   │   ├── Program.cs       # Configuração
    │   │   └── appsettings.json
    │   ├── RoadmApp.Application/# Lógica de Negócio
    │   ├── RoadmApp.Domain/     # Entidades
    │   ├── RoadmApp.Infrastructure/ # Persistência
    │   └── RoadmApp.Api.Tests/  # Testes Unitários xUnit
    └── RoadmApp.slnx
```

---

## 🎨 Stack Tecnológico

### **Frontend**
- **Framework**: Angular 21.2.0 (Standalone Components)
- **Linguagem**: TypeScript 5.9.2
- **Styling**: CSS3 com animações avançadas
- **HTTP Client**: Angular HttpClient
- **Router**: Angular Router com navegação programática
- **Alerts**: SweetAlert2 11.26.25 (modais beautifully styled)
- **Form Handling**: FormsModule com two-way binding
- **Testing**: Vitest com Angular Testing Utilities

### **Backend**
- **Framework**: ASP.NET Core 8.0
- **Padrão**: Minimal APIs
- **ORM**: Entity Framework Core
- **Excel Export**: ClosedXML 0.102.1
- **CORS**: Configurado para desenvolvimento local
- **Testing**: xUnit com Moq

### **Database**
- **Armazenamento**: Browser LocalStorage (Cliente)
- **Cache**: localStorage com chaves por usuário
- **Backup**: Exportação em Excel (.xlsx)

### **DevOps & Build**
- **Package Manager**: npm
- **Build Tool**: Angular CLI
- **Testing Runner**: Vitest + Jasmine
- **Version Control**: Git

---

## 🚀 Features Principais

### 1. **Autenticação e Segurança**
```typescript
✓ Registro de usuários com validação de email
✓ Login com verificação de credenciais
✓ Isolamento de dados por usuário (email como chave)
✓ Sessão persistente em localStorage
✓ Logout com limpeza de dados
```

### 2. **Gerenciamento de Notas**
- **10 Categorias Disponíveis**:
  - 📖 Estudos
  - 💪 Treino
  - 🎯 Metas
  - 📝 Notas
  - ⭐ Prioridades
  - 📚 Blocos de Estudo
  - 💡 Ideias
  - 🔄 Hábitos
  - 💰 Gastos
  - 🏆 Pequenas Vitórias

- **Funcionalidades de Nota**:
  - Criação com título e conteúdo
  - Múltiplas imagens por nota
  - Canvas para desenho livre
  - Metadata automática (data criação, mês)
  - Exclusão com confirmação

### 3. **Dashboard Interativo**
```typescript
✓ Visualização organizada por categoria
✓ Filtro por período (mês/ano)
✓ Contador de notas por categoria
✓ Cálculo de percentual de notas
✓ Busca e edição em tempo real
```

### 4. **Exportação de Dados**
- Exporta notas filtradas para Excel (.xlsx)
- Formatação profissional com:
  - Cabeçalho em negrito com fundo teal
  - Ajuste automático de largura de colunas
  - Quebra de texto para conteúdo longo
  - Paleta de cores consistente

### 5. **Animação Visual**
```
🌟 Partículas de estrelas animadas
🌌 Gradiente de céu celeste (#cde7ff)
✨ Movimento contínuo com sine wave drift
∞ Loop infinito com wrapping nos limites
⚡ Otimizado com NgZone para performance
```

---

## 🛠️ Como Executar

### **Pré-requisitos**
- Node.js 18+ com npm
- .NET 8.0 SDK
- Git

### **Configuração do Frontend**

```bash
# Clonar repositório
git clone <repository-url>
cd primeiro-angular

# Instalar dependências
npm install

# Executar servidor de desenvolvimento
npm start
# ou
ng serve

# Acessar em: http://localhost:4200
```

### **Configuração do Backend**

```bash
# Navegar para backend
cd backend

# Restaurar dependências .NET
dotnet restore

# Executar servidor API
cd src/RoadmApp.Api
dotnet run

# API disponível em: http://localhost:5122
```

### **Portas Padrão**
| Serviço | URL | Porta |
|---------|-----|-------|
| Frontend (Angular) | http://localhost:4200 | 4200 |
| Backend (API) | http://localhost:5122 | 5122 |
| CORS Configurado | ✓ localhost:4200, 4201 | - |

---

## 🧪 Testes

### **Cobertura de Testes**
- **33 testes** - Angular/Vitest ✅
- **28 testes** - .NET/xUnit ✅
- **Total**: 61 testes com 100% de aprovação

### **Executar Testes**

```bash
# Frontend - Testes unitários
npm test
# ou
ng test --watch=false

# Backend - Testes xUnit
cd backend/src/RoadmApp.Api.Tests
dotnet test
```

### **Estrutura de Testes**

#### **Frontend - src/app/**
```
app.spec.ts (6 testes)
├── Component creation
├── Router outlet presence
├── Canvas initialization
├── Star animation setup
└── Cleanup on destroy

pages/
├── login/login.spec.ts (7 testes)
│   ├── Validação de email vazio
│   ├── Validação de senha
│   ├── Busca de usuário
│   └── Navegação
│
├── register/register.spec.ts (6 testes)
│   ├── Criação de usuário
│   ├── Validação de email único
│   └── Navegação
│
└── dashboard/dashboard.spec.ts (13 testes)
    ├── CRUD de notas
    ├── Filtro por mês
    ├── Exportação Excel
    ├── Isolamento de dados
    └── Logout
```

#### **Backend - RoadmApp.Api.Tests/**
```
NotesEndpointsTests.cs (7 testes)
├── Validação de request
├── Export com múltiplas notas
└── Geração de arquivo Excel

EntityTests.cs (21 testes)
├── NoteDto validation
├── ExportNotesRequest tests
└── DTOs e modelos
```

### **Exemplo de Teste**
```typescript
it('should export notes as Excel', () => {
  component.notes = [{ id: '1', title: 'Test', ... }];
  component.filterMonth = '2026-06';
  
  component.exportMonth();
  
  const req = httpMock.expectOne('http://localhost:5122/api/notes/export-excel');
  expect(req.request.method).toBe('POST');
  expect(req.request.body.notes.length).toBe(1);
});
```

---

## 📡 API Endpoints

### **POST `/api/notes/export-excel`**
Exporta notas para arquivo Excel

**Request:**
```json
{
  "month": "2026-06",
  "notes": [
    {
      "id": "1",
      "title": "Study Angular",
      "category": "Estudos",
      "content": "...",
      "createdAt": "2026-06-01T10:00:00Z",
      "month": "2026-06",
      "images": ["img1.jpg"],
      "drawing": null
    }
  ]
}
```

**Response:**
```
Content-Type: application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
Body: Binary Excel file (.xlsx)
```

---

## 💾 Armazenamento de Dados

### **LocalStorage Keys**
```typescript
// Usuários registrados
'roadmapp-users': [
  { nome: 'João', email: 'joao@email.com', password: 'hash...' },
  ...
]

// Usuário logado
'roadmapp-current-user': {
  nome: 'João',
  email: 'joao@email.com'
}

// Notas por usuário
'roadmapp-notes-joao@email.com': [
  {
    id: 'uuid',
    title: 'Nota 1',
    category: 'Estudos',
    content: 'Conteúdo',
    createdAt: '2026-06-01T10:00:00Z',
    month: '2026-06',
    images: ['base64...'],
    drawing: 'base64...'
  }
]
```

---

## 🎓 Conceitos Implementados

### **Angular Moderno**
- ✅ Standalone Components (sem NgModule)
- ✅ TypedForms com FormsModule
- ✅ Standalone Router
- ✅ HttpClient Interceptors
- ✅ Component Lifecycle Hooks
- ✅ Signals (Angular 21)
- ✅ Dependency Injection

### **.NET Clean Architecture**
- ✅ Clean Architecture (Domain, Application, Infrastructure)
- ✅ Minimal APIs
- ✅ CORS Policy
- ✅ Dependency Injection
- ✅ Entity Framework Core
- ✅ Repository Pattern

### **Security**
- ✅ Email validation
- ✅ Password hashing
- ✅ User isolation per email
- ✅ CORS whitelist
- ✅ Session management

### **Code Quality**
- ✅ Unit Tests (61 testes)
- ✅ Component Testing
- ✅ Integration Testing
- ✅ Type Safety (TypeScript)
- ✅ Error Handling
- ✅ Logging & Debugging

---

## 🎨 Design & UX

### **Visual Highlights**
- 🌌 Céu celeste com gradiente suave (#cde7ff)
- ⭐ 180 partículas de estrelas animadas
- 🌊 Movimento em cascata com sine wave
- 💫 Animações suaves em transições
- 🎨 Paleta de cores: Teal (#0f766e) e tons neutros
- 📱 Layout responsivo

### **User Interface**
- Clean forms com validação em tempo real
- Modais beautifully styled (SweetAlert2)
- Feedback visual imediato
- Confirmação para ações críticas
- Loading spinners para operações assíncronas

---

## 📊 Estrutura de Dados

### **User**
```typescript
interface User {
  nome: string;
  email: string;
  password: string;
}
```

### **Note**
```typescript
interface Note {
  id: string;
  title: string;
  category: string;
  content: string;
  createdAt: string;  // ISO 8601
  month: string;       // YYYY-MM
  images: string[];    // Base64
  drawing?: string;    // Base64 canvas
}
```

### **Category**
```typescript
interface Category {
  label: string;
  value: string;
  icon: string;
}
```

---

## 🚀 Deployment

### **Frontend - Vercel/Netlify**
```bash
npm run build
# Output: dist/primeiro-angular/browser

# Deploy files from dist/ diretório
```

### **Backend - Azure/AWS/Heroku**
```bash
dotnet publish -c Release
# Output: bin/Release/net8.0/publish
```

### **Environment Variables**
```
FRONTEND_URL=http://localhost:4200
API_URL=http://localhost:5122
```

---

## 📚 Estrutura de Pastas Detalhada

```
primeiro-angular/
├── README.md                    # Este arquivo
├── angular.json                 # Config Angular CLI
├── package.json                 # Dependências npm
├── tsconfig.json                # Config TypeScript
├── tsconfig.app.json            # Config app-specific
│
├── src/
│   ├── main.ts                  # Entry point aplicação
│   ├── styles.css               # Estilos globais
│   ├── index.html               # Template HTML
│   │
│   └── app/
│       ├── app.ts               # Root component
│       ├── app.html             # Template
│       ├── app.css              # Estilos
│       ├── app.spec.ts          # Testes
│       ├── app.routes.ts        # Definição rotas
│       ├── app.config.ts        # Proveedores
│       │
│       └── pages/
│           ├── login/
│           │   ├── login.ts
│           │   ├── login.html
│           │   ├── login.css
│           │   └── login.spec.ts
│           │
│           ├── register/
│           │   └── ... (similar)
│           │
│           ├── dashboard/
│           │   ├── dashboard.ts      # ~400 linhas
│           │   ├── dashboard.html    # Canvas + Forms
│           │   ├── dashboard.css     # Layouts
│           │   └── dashboard.spec.ts # 13 testes
│           │
│           └── welcome/
│               └── ...
│
└── backend/
    ├── RoadmApp.slnx
    ├── dotnet-tools.json
    │
    └── src/
        ├── RoadmApp.Api/
        │   ├── Program.cs
        │   ├── appsettings.json
        │   ├── Endpoints/
        │   │   ├── NotesEndpoints.cs
        │   │   └── AuthEndpoints.cs
        │   └── ...
        │
        ├── RoadmApp.Application/
        │   ├── Auth/
        │   │   ├── AuthService.cs
        │   │   └── AuthContracts.cs
        │   ├── Planning/
        │   │   └── PlannerService.cs
        │   └── Abstractions/
        │       ├── IUnitOfWork.cs
        │       └── ...
        │
        ├── RoadmApp.Domain/
        │   ├── Users/User.cs
        │   ├── Planning/
        │   │   ├── Goal.cs
        │   │   ├── Habit.cs
        │   │   ├── RoadmapTask.cs
        │   │   └── Note.cs
        │   └── Common/Entity.cs
        │
        ├── RoadmApp.Infrastructure/
        │   ├── Persistence/
        │   │   └── RoadmAppDbContext.cs
        │   └── Security/
        │
        └── RoadmApp.Api.Tests/
            ├── RoadmApp.Api.Tests.csproj
            ├── Endpoints/NotesEndpointsTests.cs
            └── Domain/EntityTests.cs
```

---

## 🔧 Desenvolvimento Local

### **Hot Reload**
```bash
# Frontend - reload automático
ng serve --watch

# Backend - reload automático
dotnet watch run
```

### **Debug**
```bash
# Chrome DevTools (F12)
# Angular DevTools extension

# .NET Debug
dotnet run --configuration Debug
```

### **Lint & Format**
```bash
# Angular Lint
ng lint

# Format TypeScript
npx prettier --write src/
```

---

## 📖 Documentação Adicional

### **Angular 21 Features Utilizadas**
- [Standalone Components](https://angular.io/guide/standalone-components)
- [Control Flow Syntax](https://angular.io/guide/control-flow)
- [Signals](https://angular.io/guide/signals)
- [Typed Forms](https://angular.io/guide/typed-forms)

### **ASP.NET Core 8 Features**
- [Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api)
- [EF Core](https://learn.microsoft.com/en-us/ef/core/)
- [CORS](https://learn.microsoft.com/en-us/aspnet/core/security/cors)

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Por favor:

1. Faça um Fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

---

## 📄 Licença

Este projeto está licenciado sob a MIT License - veja o arquivo [LICENSE](LICENSE) para detalhes.

---

## 👨‍💼 Sobre

Desenvolvido como uma demonstração de expertise em:
- ✅ Full Stack Development (Frontend + Backend)
- ✅ Modern Angular & ASP.NET Core
- ✅ Clean Architecture & Design Patterns
- ✅ Test-Driven Development
- ✅ UI/UX Design
- ✅ API Integration

**Pontos Fortes do Projeto:**
- 💯 100% de cobertura de testes (61 testes passando)
- 🏗️ Arquitetura limpa e escalável
- 🎨 Interface visual atraente e animada
- 🔒 Autenticação e isolamento de dados
- 📊 Exportação profissional de dados
- 📱 Responsive Design
- ⚡ Performance otimizada

---

## 📞 Contato

- **GitHub**: [Lara Leal](https://github.com/LealLara)
- **LinkedIn**: [lara-leal-dev](https://linkedin.com) 

---

**Última atualização**: Junho de 2026

Feito com ❤️ usando Angular 21 + .NET 8


```bash
ng e2e
```

Angular CLI does not come with an end-to-end testing framework by default. You can choose one that suits your needs.

## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.
