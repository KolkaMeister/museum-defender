from pptx import Presentation
from pptx.util import Inches

# Создаем презентацию
prs = Presentation()

# Данные для слайдов
categories = {
    "Core Programming Concepts": [
        ("Bug / Defect / Issue", "Fault, Error, Glitch"),
        ("Refactoring", "Code Restructuring, Optimization"),
        ("Debugging", "Troubleshooting, Error Resolution"),
        ("Prototyping", "Mockup, Wireframing"),
        ("Unit Testing / Testing", "Validation, Verification")
    ],
    "Development and Tools": [
        ("Framework / Library", "Toolkit, SDK (Software Development Kit)"),
        ("API (Application Programming Interface)", "Web Service, Interface"),
        ("Repository / Repo", "Codebase, Version Control System (VCS)"),
        ("Compiler", "Translator, Code Generator"),
        ("Interpreter", "Executor, Runtime"),
        ("IDE (Integrated Development Environment)", "Development Environment, Code Editor"),
        ("Middleware", "Intermediary Software, Mediation Layer")
    ],
    "Architecture and Design Patterns": [
        ("Microfrontends", "Modular Frontends, Component Based Architecture"),
        ("Event Driven Architecture", "Message Driven Architecture, Reactive Architecture"),
        ("Monolithic Architecture", "Single Tiered Architecture, Unified System"),
        ("Service Oriented Architecture (SOA)", "Distributed Architecture, Modular Services"),
        ("Serverless", "Function as a Service (FaaS), Event Driven Computing"),
        ("Containerization", "Application Packaging, Isolation"),
        ("Virtualization", "Emulation, Simulation")
    ],
    "Development Methodologies": [
        ("Agile Methodology", "Iterative Development, Flexible Development"),
        ("Kanban", "Visual Management, Workflow Management"),
        ("Waterfall Model", "Sequential Development, Linear Development"),
        ("Sprint", "Iteration, Development Cycle"),
        ("Backlog", "Task List, Pending Work"),
        ("User Story", "Requirement, Feature Description"),
        ("MVP (Minimum Viable Product)", "Prototype, Beta Version")
    ],
    "Infrastructure and Performance": [
        ("Scalability", "Extensibility, Flexibility"),
        ("Load Balancing", "Traffic Distribution, Resource Allocation"),
        ("Caching", "Data Storage, Memory Management"),
        ("Continuous Integration (CI)", "Automated Integration, Build Automation"),
        ("Continuous Deployment (CD)", "Automated Deployment, Release Automation"),
        ("DevOps", "Development Operations, CI/CD")
    ],
    "Cloud Computing and Data": [
        ("Cloud / Cloud Computing", "Distributed Computing, Utility Computing"),
        ("Cloud Native", "Cloud First, Born in the Cloud"),
        ("Big Data", "Large Scale Data, Data Analytics"),
        ("Blockchain", "Distributed Ledger, Cryptographic Chain"),
        ("Container / Docker", "Virtualization, Sandbox")
    ],
    "Web Development and User Experience": [
        ("Responsive Design", "Adaptive Design, Fluid Layout"),
        ("A/B Testing", "Split Testing, Bucket Testing"),
        ("SEO (Search Engine Optimization)", "Web Optimization, Search Visibility"),
        ("UX (User Experience)", "User Journey, Interaction Design"),
        ("REST (Representational State Transfer)", "RESTful API, Web Services"),
        ("GraphQL", "Query Language, Data Fetching")
    ]
}

# Добавляем слайды в презентацию
for category, terms in categories.items():
    # Добавляем слайд с категорией
    slide = prs.slides.add_slide(prs.slide_layouts[5])
    title = slide.shapes.title
    title.text = category

    # Добавляем слайды с терминами
    for term, synonyms in terms:
        slide = prs.slides.add_slide(prs.slide_layouts[1])
        title = slide.shapes.title
        content = slide.placeholders[1]

        title.text = term
        content.text = f"Synonyms: {synonyms}\nApplication: -"

# Сохраняем презентацию
pptx_filename = "/mnt/data/software_terms_by_category.pptx"
prs.save(pptx_filename)

# Выводим ссылку для скачивания
pptx_filename
